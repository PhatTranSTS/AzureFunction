using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.HumanInteraction.Activities
{
    public class PutOnApprovedMessagesQueueActivity
    {
        [FunctionName("PutOnApprovedMessagesQueueActivity")]
        [return: Queue("approved-messages", Connection = "EventsStorageConnection")]
        public RequestSaveModel Run(
            [ActivityTrigger] RequestSaveModel message,
            ILogger log)
        {
            log.LogInformation("-----PutOnApprovedMessagesQueueActivity");
            return message;
        }
    }
}
