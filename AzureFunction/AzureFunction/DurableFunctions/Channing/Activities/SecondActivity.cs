using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class SecondActivity
    {
        [FunctionName("SecondActivity")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"SecondActivity: {name}.");
            Thread.Sleep(5000);
            return $"Process SecondActivity: {name}";
        }
    }
}
