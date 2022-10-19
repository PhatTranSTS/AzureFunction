using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.FanInFanOut
{
    public static class FanInFanOutFunction
    {
        [FunctionName("FanInFanOutFunction")]
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("Processing Fan In Fan Out Function....");

            List<string> serverNames = new List<string>() { "New York", "Paris", "Ha Noi", "Hai Phong", "Ho Chi Minh"};

            var tasks = new List<Task<string>>();
            foreach(var server in serverNames)
            {
                tasks.Add(context.CallActivityAsync<string>("CheckStatusServer", server));
            }

            await Task.WhenAll(tasks);

            return new ResponseModel()
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseString = "System OK"
            };
        }
    }
}