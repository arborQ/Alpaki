using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alpaki.SearchEngine;
using Alpaki.SearchEngine.SearchItems;
using Quartz;

namespace Alpaki.WebJob.Jobs
{
    public class HelloJob : IScheduledJob
    {
        private readonly ISearchWriteClient _searchWriteClient;

        public string Name => nameof(HelloJob);

        public string GroupName => $"{nameof(HelloJob)}Group";

        public Action<SimpleScheduleBuilder> Schedule => x => x.WithIntervalInSeconds(20).WithRepeatCount(2);

        public HelloJob(ISearchWriteClient searchWriteClient)
        {
            _searchWriteClient = searchWriteClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                await Console.Out.WriteLineAsync("Start updating index data...");

                var brands = new List<CarBrandSearchItem> {
                new CarBrandSearchItem { BrandCode = "BMW", BrandName = "BMW" },
                new CarBrandSearchItem { BrandCode = "AUD", BrandName = "Audi" },
                new CarBrandSearchItem { BrandCode = "TOY", BrandName = "Toyota" },
                new CarBrandSearchItem { BrandCode = "MRC", BrandName = "Mercedes" },
            };

                await _searchWriteClient.RebuildSearchData(brands, "test-brands-one");

            }
            catch (Exception e)
            {
                if (e == null) { }
            }
            finally
            {
                await Console.Out.WriteLineAsync("Done updating index data...");
            }

        }
    }
}