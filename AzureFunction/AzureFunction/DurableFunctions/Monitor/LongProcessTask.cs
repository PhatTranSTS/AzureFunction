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
        public static async Task<bool> Run([ActivityTrigger] int count, ILogger log)
        {
            log.LogInformation($"-------Process LongProcessTask Activity: Times - {count}");
            await Task.Delay(10000);
            return false;
        }
    }
}