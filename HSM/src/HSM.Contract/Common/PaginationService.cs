using Ardalis.Specification;
using HSM.Application.Common.Interfaces;
using HSM.Application.Common.Models;

namespace HSM.Contract.Common
{
    public class PaginationService : IPaginationService
    {
        public async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        IReadRepositoryBase<T> repository,
        ISpecification<T, TDestination> spec,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto
        {
            var list = await repository.ListAsync(spec, cancellationToken);
            int count = await repository.CountAsync(spec, cancellationToken);

            return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
        }
    }
}
