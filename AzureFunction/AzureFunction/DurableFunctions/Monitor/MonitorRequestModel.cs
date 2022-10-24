using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.Monitor
{
    public class MonitorRequestModel
    {
        public string Sendor { get; set; }
        public string InstanceId { get; set; }
    }
}
