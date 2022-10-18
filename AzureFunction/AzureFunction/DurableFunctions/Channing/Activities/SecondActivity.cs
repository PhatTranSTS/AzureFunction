using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class SecondActivity
    {
        [FunctionName("SecondActivity")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"SecondActivity: {name}.");
            return $"Process SecondActivity: {name}";
        }
    }
}
