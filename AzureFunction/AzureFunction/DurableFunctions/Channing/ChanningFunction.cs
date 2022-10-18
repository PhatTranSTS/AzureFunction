using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.Channing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace AzureFunction.DurableFunctions.Channing
{
    public static class ChanningFunction
    {
        [FunctionName("ChanningFunction")]
        public static async Task<ChanningResponseModel> RunOrchestrator(
            [OrchestrationTrigger]IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>("FirstActivity", "First Function"));
            outputs.Add(await context.CallActivityAsync<string>("SecondActivity", "Second Function"));
            outputs.Add(await context.CallActivityAsync<string>("ThirdActivity", "Third Function"));

            var result = new ChanningResponseModel()
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseString = string.Join(",", outputs)
            };
            return result;
        }
    }
}