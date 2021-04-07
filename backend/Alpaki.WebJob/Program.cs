using Alpaki.WebJob.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz.Impl;
using Quartz.Spi;

namespace Alpaki.WebJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(async (hostContext, services) =>
                {
                    var factory = new StdSchedulerFactory();
                    var scheduler = await factory.GetScheduler();

                    services.AddSingleton<IJobFactory, JobFactory>();
                    services.AddTransient<ITestService, TestService>();

                    services.AddTransient<HelloJob>();
                    services.AddTransient<HelloJobOther>();

                    services.AddSingleton(scheduler);
                    services.AddHostedService<Worker>();
                });
    }
}
