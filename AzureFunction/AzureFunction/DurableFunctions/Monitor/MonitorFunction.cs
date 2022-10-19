using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Monitor
{
    public static class MonitorFunction
    {
        [FunctionName("MonitorFunction")]
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("Processing Monitor Function....");

            //string instanceId = context.GetInput<string>();
            DateTime expTime = context.CurrentUtcDateTime.AddHours(24);

            string exportReportStatus = string.Empty;
            int number = 0;
            while (context.CurrentUtcDateTime < expTime)
            { 
                exportReportStatus = await context.CallActivityAsync<string>("ExportReport", number);

                if (exportReportStatus == "Completed")
                {
                    log.LogInformation("Done Monitor!!!");
                    break;
                } else
                {
                    number++;
                    var nextCheck = context.CurrentUtcDateTime.AddSeconds(5);
                    await context.CreateTimer(nextCheck, CancellationToken.None);
                }
            }

            //int latestPrice = await context.CallActivityWithRetryAsync<int>("GetLatestPrice",
            //        new RetryOptions(firstRetryInterval : TimeSpan.FromSeconds(4), maxNumberOfAttempts : 3),
            //        string.Empty
            //    );

            //log.LogInformation($"Latest Price: {latestPrice}");

            return new ResponseModel()
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseString = "Done"
            };
        }
    }
}
