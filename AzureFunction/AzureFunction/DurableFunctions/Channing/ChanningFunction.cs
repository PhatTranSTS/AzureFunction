using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing
{
    public static class ChanningFunction
    {
        [FunctionName("ChanningFunction")]
        public static async Task<IActionResult> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("======Processing Channing Function....");
            try
            {
                int firstValue = await context.CallActivityAsync<int>("FirstActivity", 10);

                int secondValue = await context.CallActivityAsync<int>("SecondActivity", firstValue);

                int thirdValue = await context.CallActivityAsync<int>("ThirdActivity", firstValue);

                return new OkObjectResult("------Finish Channing Function");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}