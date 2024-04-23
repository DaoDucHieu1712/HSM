using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Domain.Exceptions
{
    public class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(Guid departmentId) : base($"The department with the id {departmentId} was not found.")
        {
        }
    }
}
