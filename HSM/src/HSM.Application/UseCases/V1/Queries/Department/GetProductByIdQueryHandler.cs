using AutoMapper;
using HSM.Contract.Abstractions.Message;
using HSM.Contract.Abstractions.Shared;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.UseCases.V1.Queries.Department
{
    public class GetProductByIdQueryHandler : IQueryHandler<Query.GetDepartmentByIdQuery, Response.DepartmentResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<Response.DepartmentResponse>> Handle(Query.GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindByIdAsync(request.Id) 
                                     ?? throw new Exception("Department not found !!");
            var result = _mapper.Map<Response.DepartmentResponse>(department);
            return Result.Success(result);
        }
    }
}
