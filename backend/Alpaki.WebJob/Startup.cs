using System;
using Alpaki.CrossCutting.Jobs;
using Alpaki.Moto.Job.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.WebJob
{
    public static class Startup
    {
        public static IServiceProvider RegisterServices()
        {
            IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", true, true)
                  .Build();

            var services = new ServiceCollection();

            services.AddSingleton<IJobHandler, BrandReader>();

            return services.BuildServiceProvider(true);
        }
    }
}
