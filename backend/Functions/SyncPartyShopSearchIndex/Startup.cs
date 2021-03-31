﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Algolia.Search.Clients;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(SyncPartyShopSearchIndex.Startup))]
namespace SyncPartyShopSearchIndex
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton(services => {

                var config = services.GetService<IConfiguration>();
                var url = config.GetValue<string>("ProductsUrl");
                var searchConfig = config.GetSection("SearchIndex").Get<SearchIndexConfig>();

                return searchConfig;
            });

            builder.Services.AddTransient<ISearchClient>(_ =>
            {
                var config = _.GetService<SearchIndexConfig>();

                return new SearchClient(config.ApplicationId, config.ApiKey);
            });

            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .AddEnvironmentVariables()
                .AddJsonFile("local.settings.json", false);
        }
    }
}
