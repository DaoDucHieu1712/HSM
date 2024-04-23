using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Contract.Services.V1.Department
{
    public static class Response
    {
        public record DepartmentResponse(Guid Id, string Name, string Description);
    }
}
