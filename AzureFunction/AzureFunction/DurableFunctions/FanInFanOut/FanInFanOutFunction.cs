using System;
using System.Collections.Generic;
using System.Linq;
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
            log.LogInformation("======Processing Fan In Fan Out Function....");
            try
            {
                List<string> serverNames = new List<string>() { "New York", "Paris", "Ha Noi", "Hai Phong", "Ho Chi Minh" };
                List<string> brokenServers = new List<string>() { "New York", "Error", "Y" };

                var tasks = new List<Task<bool>>();
                foreach (var server in brokenServers)
                {
                    tasks.Add(context.CallActivityAsync<bool>("CheckStatusServer", server));
                }

                await Task.WhenAll(tasks);

                // Check status task
                if(tasks.Select(task => task.Result).ToList().Any(x => !x))
                    return new ResponseModel(HttpStatusCode.BadRequest, "System Run Failed");

                return new ResponseModel(HttpStatusCode.OK, "System OK");
            }
            catch (Exception ex)
            {
                return new ResponseModel(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}