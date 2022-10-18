using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class CheckStatusServer
    {
        [FunctionName("CheckStatusServer")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            return $"Server {name}: Good";
        }
    }
}