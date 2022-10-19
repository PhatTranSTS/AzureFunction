using System.Net;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using AzureFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.HumanInteraction
{
    public static class ApprovalRequest
    {
        [FunctionName("ApprovalRequest")]
        public static async Task<ApprovalModel> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation("Running ApprovalRequest....");
            var approvalModel = context.GetInput<ApprovalModel>();

            return approvalModel == null ? new ApprovalModel()
            {
                IsApproved = false
            } : approvalModel;
        }
    }
}
