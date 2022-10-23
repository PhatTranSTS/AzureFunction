using System.Net;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.HumanInteraction
{
    public static class CreateRequest
    {
        [FunctionName("CreateRequest")]
        public static async Task<ResponseModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("Creating Request.....");

            var approval = await context.WaitForExternalEvent<ApprovalModel>("ApprovalRqeuest");

            log.LogInformation("Waiting for approval....");

            string message = string.Empty;
            if (approval.IsApproved)
                message = "Approval";
            else
                message = "Rejected";

            var result = new ResponseModel(HttpStatusCode.OK, message);
            return result;
        }
    }
}
