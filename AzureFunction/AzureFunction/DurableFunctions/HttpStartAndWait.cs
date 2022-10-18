using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using System.Net;
using AzureFunction.Models;

namespace AzureFunction.DurableFunctions
{
    public static class HttpStartAndWait
    {
        [FunctionName("HttpStartAndWait")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "startandwait/{orchestratorName}")] HttpRequestMessage req,
            [DurableClient] IDurableClient orchestratorClient,
            string orchestratorName,
            ILogger log)
        {
            log.LogInformation("Processing HTTP START And WAIT....");
            if (string.IsNullOrEmpty(orchestratorName))
            {
                log.LogError("Orchestrator Name can not be null.");
                return new BadRequestObjectResult("Orchestrator Name can not be null. Please try again!");
                //return req.CreateResponse(HttpStatusCode.BadRequest, "Orchestrator Name can not be null. Please try again!");
            }

            try
            {
                var orchestratorInput = await req.Content.ReadAsAsync<object>();
                string instanceId = await orchestratorClient.StartNewAsync(
                    orchestratorName,
                    orchestratorInput);

                HttpResponseMessage responseMessage = await orchestratorClient.WaitForCompletionOrCreateCheckStatusResponseAsync(
                    req,
                    instanceId);

                var test = new FunctionResponseModel()
                {
                    FunctionName = orchestratorName,
                    InstanceId = responseMessage.ToString(),
                    Status = null
                };
                return new OkObjectResult(test);
            } catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
