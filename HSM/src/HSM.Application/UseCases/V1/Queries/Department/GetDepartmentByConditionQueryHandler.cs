using Ardalis.Specification;
using HSM.Application.Common.Models;
using HSM.Application.Dto;
using HSM.Application.Specifications;
using HSM.Contract.Common;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Abstractions.Repositories;
using MediatR;

namespace HSM.Application.UseCases.V1.Queries.Department
{

    public sealed class DepartmentSpec : EntitiesByPaginationFilterSpec<Domain.Entities.Department, DepartmentDto>,
    ISingleResultSpecification<Domain.Entities.Department>
    {
        public DepartmentSpec(GetDepartmentsQuerySpec request)
            : base(request)
        {
            Query.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
            });
        }
    }

    public class GetDepartmentByConditionQueryHandler(IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository, IPaginationService paginationService) : IRequestHandler<GetDepartmentsQuerySpec, ResponseBase<PaginationResponse<DepartmentDto>>>

    {
        public async Task<ResponseBase<PaginationResponse<DepartmentDto>>> Handle(GetDepartmentsQuerySpec request, CancellationToken cancellationToken)
        {
            var spec = new DepartmentSpec(request);
            var departments = await paginationService.PaginatedListAsync(
                _departmentRepository,
                spec,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            return new ResponseBase<PaginationResponse<DepartmentDto>>(departments);
        }
    }
}
