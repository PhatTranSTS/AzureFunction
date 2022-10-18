using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class FirstActivity
    {
        [FunctionName("FirstActivity")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"FirstActivity: {name}.");
            return $"Process FirstActivity: {name}";
        }
    }
}