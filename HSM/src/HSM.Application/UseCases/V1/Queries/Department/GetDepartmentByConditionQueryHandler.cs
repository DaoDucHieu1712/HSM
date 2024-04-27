using Ardalis.Specification;
using HSM.Application.Common.Models;
using HSM.Application.Dto;
using HSM.Application.Specifications;
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

    public class GetDepartmentByConditionQueryHandler : IRequestHandler<GetDepartmentsQuerySpec, ResponseBase<List<DepartmentDto>>>

    {
        private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
        public GetDepartmentByConditionQueryHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ResponseBase<List<DepartmentDto>>> Handle(GetDepartmentsQuerySpec request, CancellationToken cancellationToken)
        {
            var spec = new DepartmentSpec(request);
            var departments = await _departmentRepository.ListAsync(spec);
            return new ResponseBase<List<DepartmentDto>>(departments);
        }
    }
}
