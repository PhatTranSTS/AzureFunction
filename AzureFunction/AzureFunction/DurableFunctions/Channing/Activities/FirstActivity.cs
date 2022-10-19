using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class FirstActivity
    {
        [FunctionName("FirstActivity")]
        public static int Run([ActivityTrigger] int number, ILogger log)
        {
            log.LogInformation($"Process FirstActivity - Value: {number}");
            return number;
        }
    }
}