using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunction.CommonFunctions
{
    public class QueueFunction
    {
        [FunctionName("QueueFunction")]
        public void Run([ServiceBusTrigger("DemoQueue", Connection = "AzureQueueStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
