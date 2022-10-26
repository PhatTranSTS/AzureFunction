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
            log.LogInformation("=========Processing Monitor Functio n....");
            int count = 0;
            try
            {
                DateTime expTime = context.CurrentUtcDateTime.AddSeconds(60);
                log.LogInformation($"Expiry Time: {expTime.ToString()}");
                while (context.CurrentUtcDateTime < expTime)
                {
                    var jobStatus = await context.CallActivityAsync<bool>("LongProcessTask", count);
                    if (jobStatus)
                    {
                        log.LogInformation("Job is completed!!!");
                        break;
                    }

                    var nextCheck = context.CurrentUtcDateTime.AddSeconds(5);
                    await context.CreateTimer(nextCheck, CancellationToken.None);
                    count++;
                }

                log.LogError($"**=====MonitorFunction: TIME-OUT - at {context.CurrentUtcDateTime.ToString()}");
                return new BadRequestObjectResult("TIME-OUT");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
