using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.FanInFanOut.Models
{
    public class RequestCheckStatusServerModel
    {
        public string ServerName { get; set; }
        // Force crash for the demo of case has a not completed task
        public bool ForceCrash { get; set; }
    }
}
