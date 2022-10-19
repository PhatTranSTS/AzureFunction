using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            log.LogInformation("Processing LongFunction:....");
            Thread.Sleep(15000);
            var result = new ResponseModel()
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseString = "Done LongFunction"
            };
            return result;
        }
    }
}
