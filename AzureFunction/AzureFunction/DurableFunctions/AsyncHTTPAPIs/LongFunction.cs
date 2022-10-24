using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.AsyncHTTPAPIs
{
    public static class LongFunction
    {
        [FunctionName("LongFunction")]
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("======Processing LongFunction:....");
            log.LogInformation("Waiting for 30s....");
            await Task.Delay(30000);
            return new ResponseModel(HttpStatusCode.OK, "Done LongFunction");
        }
    }
}
