using Ardalis.Specification;
using System.Linq.Expressions;

namespace HSM.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity, in TKey>
        where TEntity : class
    {
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveMultiple(List<TEntity> entities);
    }
}
