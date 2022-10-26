using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunction.Others
{
    public class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        public void Run([QueueTrigger("approved-messages", Connection = "EventsStorageConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
