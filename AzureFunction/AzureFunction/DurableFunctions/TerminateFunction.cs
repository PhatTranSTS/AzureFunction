using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Demo.DotNetCore._02_Maintenance
{
    public class TerminateFunction
    {
        [FunctionName("TerminateFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "POST", Route = "terminate/{instanceId}")] HttpRequestMessage request,
            [DurableClient] IDurableClient client,
            string instanceId,
            ILogger log)
        {
            log.LogInformation($"Terminated Function {instanceId}");
            var reason = await request.Content.ReadAsAsync<string>();
            await client.TerminateAsync(instanceId, reason);

            return new AcceptedResult();
        }
    }
}
