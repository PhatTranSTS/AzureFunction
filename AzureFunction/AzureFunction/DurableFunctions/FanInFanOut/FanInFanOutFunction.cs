using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.FanInFanOut.Models;
using AzureFunction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.FanInFanOut
{
    public static class FanInFanOutFunction
    {
        [FunctionName("FanInFanOutFunction")]
        public static async Task<IActionResult> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("======Processing Fan In Fan Out Function....");
            try
            {
                var input = context.GetInput<RequestFIFOModel>();

                List<string> serverNames = new List<string>() { "New York", "Paris", "Ha Noi", "Hai Phong", "Ho Chi Minh" };

                var tasks = new List<Task<bool>>();
                foreach (var server in serverNames)
                {
                    tasks.Add(context.CallActivityAsync<bool>("CheckStatusServerActivity", new RequestCheckStatusServerModel()
                    {
                        ServerName = server,
                        ForceCrash = input.IsForceCrash
                    }));
                }

                await Task.WhenAll(tasks);

                // Check is any task crashed
                bool isHasTaskCrashed = tasks.Select(task => task.Result).ToList().Any(x => !x);
                if (isHasTaskCrashed)
                    return new BadRequestObjectResult("System Run Failed");

                return new OkObjectResult("System OK");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}