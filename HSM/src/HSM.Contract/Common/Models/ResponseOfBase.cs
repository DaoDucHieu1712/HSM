using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Common.Models
{

    public abstract class ResponseOfBase
    {
        /// <summary>
        /// Indicates success status of the result.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates message of the result.
        /// </summary>
        public string Message { get; set; }
    }
}
