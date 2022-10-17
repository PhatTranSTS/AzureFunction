using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.FunctionChaining.Activities
{
    public static class GetStoreInformation
    {
        [FunctionName("GetStoreInformation")]
        public static string SayHello([ActivityTrigger] string storeName, ILogger log)
        {
            log.LogInformation($"Get Store Information processing...");
            return $"Hello {storeName}!";
        }
    }
}