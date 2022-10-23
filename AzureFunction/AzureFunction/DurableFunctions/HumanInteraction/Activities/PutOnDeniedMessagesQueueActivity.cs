using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
namespace AzureFunction.DurableFunctions.HumanInteraction.Activities
{
    class PutOnDeniedMessagesQueueActivity
    {
        [FunctionName("PutOnDeniedMessagesQueueActivity")]
        [return: Queue("denied-messages", Connection = "EventsStorageConnection")]
        public RequestSaveModel Run(
            [ActivityTrigger] RequestSaveModel message,
            ILogger log)
        {
            log.LogInformation("-----PutOnDeniedMessagesQueueActivity");
            return message;
        }
    }
}
