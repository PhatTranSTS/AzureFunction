using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class SecondActivity
    {
        [FunctionName("SecondActivity")]
        public static int Run([ActivityTrigger] int number, ILogger log)
        {
            log.LogInformation($"Process SecondActivity - Value: {number}");
            Random random = new Random();
            return random.Next(100, 1000);
        }
    }
}
