using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using System.Net;

namespace AzureFunction.DurableFunctions
{
    public static class HttpStartAndWait
    {
        [FunctionName("HttpStartAndWait")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "startandwait/{orchestratorName}")] HttpRequestMessage req,
            [DurableClient]IDurableClient orchestratorClient,
            string orchestratorName,
            ILogger log)
        {
            log.LogInformation("Processing HTTP START And WAIT....");
            if (string.IsNullOrWhiteSpace(orchestratorName))
            {
                log.LogError("Orchestrator Name can not be null.");
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Orchestrator Name can not be null. Please try again!")
                };
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

                return responseMessage;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ex.Message)
                };
            }
        }
    }
}
