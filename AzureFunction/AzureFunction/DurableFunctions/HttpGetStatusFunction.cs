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

namespace AzureFunction.DurableFunctions
{
    public static class HttpGetStatusFunction
    {
        [FunctionName("HttpGetStatusFunction")]
        public static async Task<DurableOrchestrationStatus> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "status/{id}")] HttpRequestMessage req,
            [DurableClient] IDurableClient orchestratorClient,
            string id,
            ILogger log)
        {
            log.LogInformation("HTTP Get Status processing...");
            if(string.IsNullOrEmpty(id))
            {
                log.LogError("Id can not be null.");
                return null;
            }

            try
            {
                DurableOrchestrationStatus status = await orchestratorClient.GetStatusAsync(id,true, true, true);
                return status;
            } catch(Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }
    }
}
