using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.Models
{
    public class FunctionStatusModel
    {
        public string InstanceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public OrchestrationRuntimeStatus RunTimeStatus { get; set; }
    }
}
