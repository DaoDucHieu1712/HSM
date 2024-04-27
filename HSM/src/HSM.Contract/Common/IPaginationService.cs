using Ardalis.Specification;
using HSM.Application.Common.Interfaces;
using HSM.Application.Common.Models;

namespace HSM.Contract.Common
{
    public interface IPaginationService
    {
        Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        IReadRepositoryBase<T> repository,
        ISpecification<T, TDestination> spec,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto;
    }
}
