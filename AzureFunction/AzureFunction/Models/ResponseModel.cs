using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AzureFunction.Models
{
    public class ResponseModel
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ResponseString { get; set; }
    }
}
