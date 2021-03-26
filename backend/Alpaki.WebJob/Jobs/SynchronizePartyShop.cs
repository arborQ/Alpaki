using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Alpaki.SearchEngine;
using Alpaki.WebJob.Models;
using CsvHelper;
using Quartz;

namespace Alpaki.WebJob.Jobs
{
    public class SynchronizePartyShop : IScheduledJob
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly ISearchWriteClient _searchWriteClient;
        private readonly SearchClient _angoliaClient;

        public string Name => "SynchronizePartyShop";

        public string GroupName => "PartyShop";

        public Action<SimpleScheduleBuilder> Schedule => x => x.WithIntervalInHours(24).RepeatForever();

        public SynchronizePartyShop(ISearchWriteClient searchWriteClient)
        {
            _searchWriteClient = searchWriteClient;

            _angoliaClient = new SearchClient("SR9N5725NZ", "5b8c3d0c7172da4de6a634f1413fc62f");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var indexName = "party-shop-index";
                var response = await _httpClient.GetAsync("https://shop.partydeco.pl/integrationFiles/20998/ab5ec542160e4bf706a77ca964f7c7ec7704e3f29858f8b43a3477bface010e5/products?format=csv&language=pl");

                using TextReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());

                //var streamReader = File.OpenText(@"C:\Users\lukasz.wojcik\Alpaki\backend\Alpaki.WebJob\products.csv");
                var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                var items = reader.GetRecords<PartyItem>().ToList();

                await _searchWriteClient.RebuildSearchData(items, indexName);

                var index = _angoliaClient.InitIndex(indexName);
                await index.SaveObjectsAsync(items);

                return;

            }
            catch (Exception e)
            {
                if (e == null) { }
            }

            throw new NotImplementedException();
        }
    }
}
