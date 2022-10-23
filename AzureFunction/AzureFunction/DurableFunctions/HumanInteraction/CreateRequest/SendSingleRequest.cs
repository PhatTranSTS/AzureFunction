using System;
using System.Threading.Tasks;
using AzureFunction.DurableFunctions.HumanInteraction.Activities;
using AzureFunction.DurableFunctions.HumanInteraction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.HumanInteraction.CreateRequest
{
    public class SendSingleRequest
    {
        [FunctionName("SendSingleRequest")]
        public async Task Run(
               [OrchestrationTrigger] IDurableOrchestrationContext context,
               ILogger log)
        {
            try
            {
                log.LogInformation("=====Processing SendSingleRequest:...");

                var request = context.GetInput<RequestApprovalModel>();

                if (request == null)
                    throw new ArgumentException("Request is NULL");

                var defaultApproval = new ApprovalModel(context.InstanceId, true, string.Empty);

                var approval = await context.WaitForExternalEvent<ApprovalModel>(
                    request.PackageName);

                var putOnQueueActivity = approval.IsApproval ?
                   nameof(PutOnApprovedMessagesQueueActivity) :
                   nameof(PutOnDeniedMessagesQueueActivity);

                log.LogInformation("SendSingleRequest: SAVE REQUEST STATUS");
                var requestSave = new RequestSaveModel(approval.RequestId, request.Sender, request.Content, request.PackageName);
                await context.CallActivityAsync<RequestSaveModel>(
                   putOnQueueActivity,
                   requestSave);

                context.SetCustomStatus(new { QueueActivityName = putOnQueueActivity });
            } catch(Exception ex)
            {
                log.LogError($"SendSingleRequest ERROR: {ex.Message}");
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
