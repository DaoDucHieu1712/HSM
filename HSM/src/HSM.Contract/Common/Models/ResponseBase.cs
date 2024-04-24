using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Common.Models
{

    /// <summary>
    /// This class is used to create standard responses for AJAX requests.
    /// </summary>
    [Serializable]
    public class ResponseBase<TResult> : ResponseOfBase
    {
        /// <summary>
        /// The actual result object of AJAX request.
        /// It is set if <see cref="ResponseOfBase.Success"/> is true.
        /// </summary>
        public TResult? Result { get; set; }

        /// <summary>
        /// Creates an <see cref="ResponseBase"/> object with <see cref="Result"/> specified.
        /// <see cref="ResponseOfBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request.</param>
        public ResponseBase(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="ResponseBase"/> object with <see cref="Result"/> specified.
        /// <see cref="ResponseOfBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request.</param>
        /// <param name="message">The actual message object of AJAX request.</param>
        public ResponseBase(TResult result, string message)
        {
            Result = result;
            Success = true;
            Message = message;
        }

        /// <summary>
        /// Creates an <see cref="ResponseBase"/> object.
        /// <see cref="ResponseOfBase.Success"/> is set as true.
        /// </summary>
        public ResponseBase()
        {
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="ResponseBase"/> object with <see cref="ResponseOfBase.Success"/> specified.
        /// <see cref="ResponseOfBase.Success"/> is set as true.
        /// </summary>
        /// <param name="message">The actual result object of AJAX request.</param>
        public ResponseBase(string message)
        {
            Message = message;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="ResponseBase"/> object with <see cref="ResponseOfBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result.</param>
        public ResponseBase(bool success)
        {
            Success = success;
        }
    }
}
