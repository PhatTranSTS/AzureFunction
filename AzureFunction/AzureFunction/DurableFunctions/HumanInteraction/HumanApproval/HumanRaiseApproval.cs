using System;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.HumanInteraction.HumanApproval
{
    public class HumanRaiseApproval
    {
        [FunctionName("HumanRaiseApproval")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "approval/{eventName}/{instanceId}")]
            HttpRequestMessage req,
            [DurableClient] IDurableClient client,
            string eventName,
            string instanceId,
            ILogger log)
        {
            log.LogInformation("======Start Processing HumanRaiseApproval:......");

            try
            {
                var approval = await req.Content.ReadAsAsync<ApprovalModel>();

                await client.RaiseEventAsync(instanceId, eventName, approval);
                return new OkObjectResult(approval);
            } catch(Exception ex)
            {
                log.LogError($"HumanRaiseApproval Error: {ex.Message}");
                throw new ArgumentException(ex.Message);
            }   
        }
    }
}
