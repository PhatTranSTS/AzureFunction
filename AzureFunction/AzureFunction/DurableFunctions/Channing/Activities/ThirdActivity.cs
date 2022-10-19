using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;

namespace AzureFunction.DurableFunctions.Channing.Activities
{
    public static class ThirdActivity
    {
        [FunctionName("ThirdActivity")]
        public static int Run([ActivityTrigger] int number, ILogger log)
        {
            log.LogInformation($"Process ThirdActivity - Value {number}.");
            Random random = new Random();
            return random.Next(100, 1000);
        }
    }
}
