using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.HumanInteraction.Models
{
    public class RequestApprovalModel
    {
        public string Sender { get; set; }
        public string Content { get; set; }
        public string PackageName { get; set; }
    }

    public class RequestSaveModel : RequestApprovalModel
    {
        public string InstancedId { get; set; }
        public RequestSaveModel(string instancedId, string sender, string content, string packageName)
        {
            this.Sender = sender;
            this.Content = content;
            this.PackageName = packageName;
            this.InstancedId = instancedId;
        }
    }
}
