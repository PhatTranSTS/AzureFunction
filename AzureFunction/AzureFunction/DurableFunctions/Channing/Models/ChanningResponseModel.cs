using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AzureFunction.DurableFunctions.Channing.Models
{
    public class ChanningResponseModel
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ResponseString { get; set; }
    }
}
