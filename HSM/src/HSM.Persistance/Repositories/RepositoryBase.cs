using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using HSM.Domain.Abstractions.Entities;
using HSM.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HSM.Persistance.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : Entity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISpecificationEvaluator _specificationEvaluator;

        public RepositoryBase(ApplicationDbContext dbContext) : this(dbContext, SpecificationEvaluator.Default)
        {
        }
        public RepositoryBase(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        {
            _dbContext = dbContext;
            _specificationEvaluator = specificationEvaluator;
        }

        public void Dispose()
            => _dbContext?.Dispose();

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _dbContext.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);

            if (predicate is not null)
                items = items.Where(predicate);

            return items;
        }

        public async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }

        public async Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }

        protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification, evaluateCriteriaOnly);
        }

        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
        }


        public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
            => await FindAll(null, includeProperties)
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
            => await FindAll(null, includeProperties)
            .AsTracking()
            .SingleOrDefaultAsync(predicate, cancellationToken);

        public void Add(TEntity entity)
            => _dbContext.Add(entity);

        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void RemoveMultiple(List<TEntity> entities)
            => _dbContext.Set<TEntity>().RemoveRange(entities);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);
    }
}
