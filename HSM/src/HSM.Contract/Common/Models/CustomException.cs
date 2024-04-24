using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Common.Models
{
    public class CustomException(string message, List<string>? errors = default,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : Exception(message)
    {
        public List<string>? ErrorMessages { get; } = errors;

        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
