using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunction.Others
{
    public class TimerTrigger
    {
        [FunctionName("TimerTrigger")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            // {second} {minute} {hour} {day} {month} {day-of-week}
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
