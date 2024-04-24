using HSM.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Dto
{
    public class DepartmentDto : IDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
