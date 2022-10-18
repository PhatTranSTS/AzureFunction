using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class ThirdActivity
    {
        [FunctionName("ThirdActivity")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"ThirdActivity: {name}.");
            return $"Process ThirdActivity: {name}";
        }
    }
}
