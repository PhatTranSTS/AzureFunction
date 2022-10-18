using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.Models
{
    public class FunctionResponseModel
    {
        public string FunctionName { get; set; }
        public string InstanceId { get; set; }
        public DurableOrchestrationStatus Status { get; set; }
    }
}
