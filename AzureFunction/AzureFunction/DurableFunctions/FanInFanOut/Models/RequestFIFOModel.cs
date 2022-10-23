using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.DurableFunctions.FanInFanOut.Models
{
    public class RequestFIFOModel
    {
        public bool IsForceCrash { get; set; }
    }
}
