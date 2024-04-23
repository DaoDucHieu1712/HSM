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
    public class DeleteDepartmentCommandHandler : ICommandHandler<Command.DeleteDepartmentCommand>
    {

        private readonly IRepositoryBase<HSM.Domain.Entities.Department, Guid> _departmentRepository;

        public DeleteDepartmentCommandHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Result> Handle(Command.DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {

            var department = await _departmentRepository.FindByIdAsync(request.Id) ?? throw new Exception("Department not found !!");
            department.Delete();
            _departmentRepository.Remove(department);
            return Result.Success();
        }
    }
}
