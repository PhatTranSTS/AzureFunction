using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.HumanInteraction.Models
{
    public class ApprovalModel
    {
        public string RequestId { get; set; }
        public bool IsApproval { get; set; }
        public string Reason { get; set; }
        public ApprovalModel(string requestId, bool isApproval, string reason)
        {
            this.RequestId = requestId;
            this.IsApproval = isApproval;
            this.Reason = reason;
        }
    }
}
