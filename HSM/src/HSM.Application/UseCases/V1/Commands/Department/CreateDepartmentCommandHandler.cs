using HSM.Contract.Abstractions.Message;
using HSM.Contract.Abstractions.Shared;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.UseCases.V1.Commands.Department
{
    public class CreateDepartmentCommandHandler : ICommandHandler<Command.CreateDepartmentCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;

        public CreateDepartmentCommandHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Result> Handle(Command.CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = Domain.Entities.Department.CreateDepartment(Guid.NewGuid(), request.Name, request.Description);
            _departmentRepository.Add(department);
            return Result.Success();
        }
    }
}
