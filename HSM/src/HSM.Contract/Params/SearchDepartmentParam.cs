using HSM.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Params
{
    public class SearchDepartmentParam : PaginationFilter
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
