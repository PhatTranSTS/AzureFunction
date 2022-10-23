using AzureFunction.DurableFunctions.FanInFanOut.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class CheckStatusServerActivity
    {
        [FunctionName("CheckStatusServerActivity")]
        public static bool RunActivity([ActivityTrigger] RequestCheckStatusServerModel input, ILogger log)
        {
            log.LogInformation("-----Running CheckStatusServer Activity");

            if (input == null)
            {
                log.LogError("Run CheckStatusServerActivity error: Input NULL");
                throw new ArgumentException("Server Information can not be NULL");
            }

            log.LogInformation($"CheckStatusServer: {input.ServerName}");

            return !input.ForceCrash;
        }
    }
}