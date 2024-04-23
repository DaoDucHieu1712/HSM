using AutoMapper;
using HSM.Contract.Abstractions.Message;
using HSM.Contract.Abstractions.Shared;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.UseCases.V1.Queries.Department
{
    public class GetProductsQueryHandler : IQueryHandler<Query.GetDepartmentsQuery, List<Response.DepartmentResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<Response.DepartmentResponse>>> Handle(Query.GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.FindAll().ToListAsync();
            var result = _mapper.Map<List<Response.DepartmentResponse>>(departments);
            return Result.Success(result);
        }
    }
}
