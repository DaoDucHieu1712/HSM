using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Common.Models
{
    public class Search
    {
        public List<string> Fields { get; set; } = new();
        public string? Keyword { get; set; }
    }
}
