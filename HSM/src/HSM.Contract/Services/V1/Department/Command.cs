using HSM.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Contract.Services.V1.Department
{
    public static class Command
    {
        public record CreateDepartmentCommand(string Name, string Description) : ICommand; 
        public record UpdateDepartmentCommand(Guid Id ,string Name, string Description) : ICommand; 
        public record DeleteDepartmentCommand(Guid Id) : ICommand; 
    }
}
