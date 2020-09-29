using System;
using Alpaki.Common.Jobs;
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
                  .AddJsonFile("appsettings.json", false, true)
                  .AddJsonFile("appsettings.local.json", true, false)
                  .Build();

            var services = new ServiceCollection();
            CommonJobRegister.Initialize(services, config);
            services.AddSingleton<IJobHandler, BrandReader>();

            return services.BuildServiceProvider(true);
        }
    }
}
