using System;
using System.Collections.Generic;
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
using static SyncPartyShopSearchIndex.Models.PartyShopIndexItem;

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


                log.LogInformation($"C# Timer trigger function executed items: {items.Count}");

                var indexItems = GetIndexItems(items);
                var index = _searchClient.InitIndex(_searchIndexConfig.IndexName);

                log.LogInformation($"C# Timer trigger function executed items: {items.Count}, groups: {indexItems.Count()}");

                var indexResult = await index.SaveObjectsAsync(indexItems);

                log.LogInformation($"Search index function executed items: {indexResult.Responses.Count}");
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

        private IEnumerable<PartyShopIndexItem> GetIndexItems(IReadOnlyCollection<PartyItem> partyItems)
        {
            var groups = partyItems
                .Select(i => new { ProductName = i.name.Split(',').First(), Item = i })
                .GroupBy(a => new { a.ProductName })
                .Select(a =>
                {
                    var productName = a.Key.ProductName;
                    var key = productName.GetHashCode().ToString();
                    var net_prices = a.Select(p => decimal.Parse(p.Item.price_net)).Distinct();
                    var gross_prices = a.Select(p => decimal.Parse(p.Item.price_gross)).Distinct();
                    var images = a.SelectMany(p => p.Item.photos.Split(';')).ToArray();

                    return new PartyShopIndexItem
                    {
                        Key = key,
                        ProductName = productName,
                        FromPriceNet = net_prices.Min(),
                        ToPriceNet = net_prices.Max(),
                        FromPriceGross = gross_prices.Min(),
                        ToPriceGross = gross_prices.Max(),
                        ImageUrls = images,
                        Variants = a.Select(p => new PartyShopIndexItemVariant
                        {
                            VariantName = p.Item.name,
                            PriceNet = decimal.Parse(p.Item.price_net),
                            PriceGross = decimal.Parse(p.Item.price_gross)
                        }).ToArray()
                    };
                });

            return groups;
        }
    }
}
