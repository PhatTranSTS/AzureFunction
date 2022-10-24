using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class LongProcessTask
    {
        [FunctionName("LongProcessTask")]
        public static async Task<bool> Run([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Process LongProcessTask Activity-----");
            await Task.Delay(30000);
            return false;
        }
    }
}