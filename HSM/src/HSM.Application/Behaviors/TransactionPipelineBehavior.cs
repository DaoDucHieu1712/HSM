using HSM.Domain.Abstractions;
using HSM.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Behaviors
{
    public sealed class TransactionPipelineBehavior
    <TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork; // MySQL-STRATEGY-2
        private readonly ApplicationDbContext _context; // MySQL-STRATEGY-1

        public TransactionPipelineBehavior(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!IsCommand()) // In case TRequest is QueryRequest just ignore
                return await next();

            #region ============== MySQL-STRATEGY-1 ==============

            //// Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //// https://learn.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                {
                    var response = await next();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return response;
                }
            });

            #endregion ============== MySQL-STRATEGY-1 ==============

            #region ============== MySQL-STRATEGY-2 ==============

            //IMPORTANT: passing "TransactionScopeAsyncFlowOption.Enabled" to the TransactionScope constructor. This is necessary to be able to use it with async/await.
            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    var response = await next();
            //    await _unitOfWork.SaveChangesAsync(cancellationToken);
            //    transaction.Complete();
            //    await _unitOfWork.DisposeAsync();
            //    return response;
            //}

            #endregion ============== MySQL-STRATEGY-2 ==============
        }

        private bool IsCommand()
            => typeof(TRequest).Name.EndsWith("Command");
    }
}