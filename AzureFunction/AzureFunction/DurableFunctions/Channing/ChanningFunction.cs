using System.Collections.Generic;
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
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger]IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("Processing Channing Function....");

            int firstValue = await context.CallActivityAsync<int>("FirstActivity", 10);
            int secondValue = await context.CallActivityAsync<int>("SecondActivity", firstValue);
            int thirdValue = await context.CallActivityAsync<int>("ThirdActivity", secondValue);

            int finalValue = firstValue + secondValue + thirdValue;
            //outputs.Add();
            //outputs.Add(await context.CallActivityAsync<string>("SecondActivity", "Second Function"));
            //outputs.Add(await context.CallActivityAsync<string>("ThirdActivity", "Third Function"));

            var result = new ResponseModel()
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseString = finalValue.ToString()
            };
            return result;
        }
    }
}