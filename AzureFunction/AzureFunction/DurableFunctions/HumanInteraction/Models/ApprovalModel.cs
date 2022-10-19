using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.HumanInteraction.Models
{
    public class ApprovalModel
    {
        public string Name { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}
