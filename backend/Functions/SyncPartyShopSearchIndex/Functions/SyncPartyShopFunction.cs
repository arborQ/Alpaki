using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Alpaki.SearchEngine;
using Azure.Search.Documents.Indexes;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SyncPartyShopSearchIndex.Models;
using static SyncPartyShopSearchIndex.Models.PartyShopIndexItem;

namespace SyncPartyShopSearchIndex.Functions
{
    public class SyncCognitivePartyShopFunction
    {

    }

    public class IndexModel
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string Id { get; set; }

        [SearchableField(IsSortable = true)]
        public string Name { get; set; }
    }

    public class SyncPartyShopFunction
    {
        private readonly ISearchWriteClient _searchWriteClient;

        public SyncPartyShopFunction(ISearchWriteClient searchWriteClient)
        {
            _searchWriteClient = searchWriteClient;
        }

        [FunctionName("SyncAzurePartyShop")]
        public async Task Run([TimerTrigger("0 1 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                var items = Enumerable.Range(0, 1000).Select(e => new IndexModel { Id = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString().Substring(0, 5) }).ToList();

                await _searchWriteClient.RebuildSearchData(items, "party-shop-test");
            }
            catch (Exception e)
            {
                log.LogError(e, "C# Timer trigger function failed");
            }
        }

        [FunctionName("SyncAzurePartyShopTrigger")]
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
                    var images = a.SelectMany(p => p.Item.photos.Split(';')).Take(1).ToArray();
                    var description = a.First().Item.description;
                    var category = a.First().Item.category_path;

                    return new PartyShopIndexItem
                    {
                        Key = key,
                        ProductName = productName,
                        FromPriceNet = net_prices.Min(),
                        ToPriceNet = net_prices.Max(),
                        FromPriceGross = gross_prices.Min(),
                        ToPriceGross = gross_prices.Max(),
                        ImageUrls = images,
                        ProductDescription = description,
                        CategoryPath = category,
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
