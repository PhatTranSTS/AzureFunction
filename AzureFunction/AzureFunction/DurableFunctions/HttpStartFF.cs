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
        /// <summary>
        /// This function starts a new Orchestrator in the same Function App
        /// The method return instanceId of new Function running
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orchestratorClient"></param>
        /// <param name="orchestratorName"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("HttpStart")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "start/{orchestratorName}")] HttpRequestMessage req,
            [DurableClient] IDurableClient orchestratorClient,
            string orchestratorName,
            ILogger log)
        {
            log.LogInformation("Processing HTTP START FF....");
            if (string.IsNullOrEmpty(orchestratorName))
            {
                log.LogError("Orchestrator Name can not be null.");
                return new BadRequestObjectResult("Orchestrator Name can not be null. Please try again!");
            }

            try
            {
                var orchestratorInput = await req.Content.ReadAsAsync<object>();
                string instanceId = await orchestratorClient.StartNewAsync(orchestratorName, orchestratorInput);

                return new OkObjectResult(instanceId);
            }
            catch (Exception ex)
            {
                log.LogError($"Error start {orchestratorName} function: {ex.Message}");
                //return req.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
