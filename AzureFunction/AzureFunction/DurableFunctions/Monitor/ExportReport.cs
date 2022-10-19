using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunction.DurableFunctions.Monitor
{
    public static class ExportReport
    {
        [FunctionName("ExportReport")]
        public static async Task<string> Run(
            [ActivityTrigger] int number, ILogger log)
        {
            //Thread.Sleep(15000);
            log.LogInformation($"Recheck: {number}");
            return number == 3 ? "Completed" : "Processing";
        }
    }
}