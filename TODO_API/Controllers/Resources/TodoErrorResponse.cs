using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Controllers.Resources
{
    public class TodoErrorResponse
    {
        public TodoErrorResponse(int errorCode, string errMessage)
        {
            ErrorCode = errorCode;
            ErrorDescription = errMessage;
           
        }

        [Newtonsoft.Json.JsonProperty("errorCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"[\w]+")]
        public int ErrorCode { get; set; }

        /// <summary>Error Description</summary>
        [Newtonsoft.Json.JsonProperty("errorDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"[\w]+")]
        public string ErrorDescription { get; set; }
    }
}
