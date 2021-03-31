using System;
using System.Net.Http;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SyncPartyShopSearchIndex
{
    public class SyncPartyShopFunction
    {
        private readonly HttpClient _httpClient;
        private readonly ISearchClient _searchClient;

        public SyncPartyShopFunction(HttpClient httpClient, ISearchClient searchClient)
        {
            _httpClient = httpClient;
            _searchClient = searchClient;
        }

        [FunctionName("SyncPartyShop")]
        public void Run([TimerTrigger("1 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        [FunctionName("SyncPartyShopTrigger")]
        public async Task<IActionResult> RunEndpoint(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //var response = await _client.GetAsync("https://microsoft.com");
            //var message = _service.GetMessage();

            //return new OkObjectResult("Response from function with injected dependencies.");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await Task.Delay(1);

            return new OkObjectResult("Response from function with injected dependencies.");
        }
    }
}
