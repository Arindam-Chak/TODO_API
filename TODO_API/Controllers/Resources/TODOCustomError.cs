using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Controllers.Resources
{
    public class TODOCustomError : Exception
    {
        public int ResponseStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public TODOCustomError(string message, int httpStatusCode = StatusCodes.Status412PreconditionFailed)
        {
            ResponseStatusCode = httpStatusCode;
            ErrorMessage = message;
        }
    }
}
