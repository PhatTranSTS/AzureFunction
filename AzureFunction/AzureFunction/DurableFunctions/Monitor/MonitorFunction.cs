using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.FanInFanOut.Models;
using AzureFunction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Monitor
{
    public static class MonitorFunction
    {
        [FunctionName("MonitorFunction")]
        public static async Task<IActionResult> RunOrchestrator(
               [OrchestrationTrigger] IDurableOrchestrationContext context,
               [DurableClient] IDurableClient orchestratorClient,
               ILogger log)
        {
            log.LogInformation("======Processing Monitor Function....");
            try
            {
                // Post man excute Long Process Task with HTTP Start FF to get instanceId
                MonitorRequestModel request = context.GetInput<MonitorRequestModel>();

                DateTime expTime = context.CurrentUtcDateTime.AddSeconds(60);

                while (context.CurrentUtcDateTime < expTime)
                {
                    log.LogInformation("TEST");
                    //var jobStatus = await orchestratorClient.GetStatusAsync(request.InstanceId, true, true, true);
                    var jobStatus = await context.CallActivityAsync<bool>("LongProcessTask", null);
                    if (jobStatus)
                    {
                        log.LogInformation("Job is completed!!!");
                        break;
                    }

                    var nextCheck = context.CurrentUtcDateTime.AddSeconds(5);
                    await context.CreateTimer(nextCheck, CancellationToken.None);
                }

                return new OkObjectResult("Completed Monitor");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
