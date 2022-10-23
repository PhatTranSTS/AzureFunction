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
            Random random = new Random();
            var result = number + random.Next(100, 1000);
            log.LogInformation($"Process ThirdActivity | Input: {number} Output: {result} |");

            return result;
        }
    }
}
