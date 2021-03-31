using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SyncPartyShopSearchIndex.Models;

namespace SyncPartyShopSearchIndex
{
    public class SyncPartyShopFunction
    {
        private readonly HttpClient _httpClient;
        private readonly ISearchClient _searchClient;
        private readonly SearchIndexConfig _searchIndexConfig;

        public SyncPartyShopFunction(HttpClient httpClient, ISearchClient searchClient, SearchIndexConfig searchIndexConfig)
        {
            _httpClient = httpClient;
            _searchClient = searchClient;
            _searchIndexConfig = searchIndexConfig;
        }

        [FunctionName("SyncPartyShop")]
        public async Task Run([TimerTrigger("0 1 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                var response = await _httpClient.GetAsync(_searchIndexConfig.ProductsUrl);

                using TextReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());

                var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                var items = reader.GetRecords<PartyItem>().ToList();

                var test = items.GroupBy(a => a.name.Split(',').First());

                log.LogInformation($"C# Timer trigger function executed items: {items.Count}, groups: {test.Count()}");

                //var index = _searchClient.InitIndex(_searchIndexConfig.IndexName);
                //var indexResult = await index.SaveObjectsAsync(items);
                
                //log.LogInformation($"Search index function executed items: {indexResult.Responses.Count}");
            }
            catch (Exception e)
            {
                log.LogError(e, "C# Timer trigger function failed");
            }
        }

        [FunctionName("SyncPartyShopTrigger")]
        public async Task<IActionResult> RunEndpoint(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await Run(null, log);

            return new OkObjectResult("Response from function with injected dependencies.");
        }
    }
}
