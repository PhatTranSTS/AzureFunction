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
            Random random = new Random();
            int result = number + random.Next(100, 1000);
            log.LogInformation($"Process SecondActivity - | Input: {number} Output: {result} |");
            
            return result;
        }
    }
}
