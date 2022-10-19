using System;
using System.Net;
using System.Threading.Tasks;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Channing
{
    public static class ChanningFunction
    {
        [FunctionName("ChanningFunction")]
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("======Processing Channing Function....");
            try
            {
                log.LogInformation("------Running FirstActivity....");
                int firstValue = await context.CallActivityAsync<int>("FirstActivity", 10);
                log.LogInformation($"+++++ FirstActivity response: {firstValue}");

                log.LogInformation("------Running SecondActivity....");
                int secondValue = await context.CallActivityAsync<int>("SecondActivity", firstValue);
                log.LogInformation($"+++++ SecondActivity response: {secondValue}");

                log.LogInformation("------Running ThirdActivity....");
                int thirdValue = await context.CallActivityAsync<int>("ThirdActivity", firstValue);
                log.LogInformation($"+++++ ThirdActivity response: {thirdValue}");

                return new ResponseModel(HttpStatusCode.OK, "Finish Channing Function");
            }
            catch (Exception ex)
            {
                return new ResponseModel(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}