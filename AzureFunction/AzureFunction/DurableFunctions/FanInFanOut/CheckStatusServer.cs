using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class CheckStatusServer
    {
        [FunctionName("CheckStatusServer")]
        public static bool SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation("-----Running CheckStatusServer Activity...");
            return name == "Error";
        }
    }
}