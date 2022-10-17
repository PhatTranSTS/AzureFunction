using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net;

namespace AzureFunction.DurableFunctions
{
    public static class HttpStartFF
    {
        [FunctionName("HttpStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "start/{orchestratorName}/")] HttpRequestMessage req,
            [DurableClient] IDurableClient orchestratorClient,
            string orchestratorName,
            ILogger log)
        {
            log.LogInformation("Processing HTTP START function....");
            if (string.IsNullOrEmpty(orchestratorName))
            {
                log.LogError("Orchestrator Name can not be null.");
                return req.CreateResponse(HttpStatusCode.NotFound, "Orchestrator Name can not be null. Please try again!");
            }

            try
            {
                var orchestratorInput = await req.Content.ReadAsAsync<object>();
                string instanceId = await orchestratorClient.StartNewAsync(orchestratorName, orchestratorInput);

                return req.CreateErrorResponse(HttpStatusCode.OK, instanceId);
            }
            catch (Exception ex)
            {
                log.LogError($"Error start {orchestratorName} function: {ex.Message}");
                return req.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
