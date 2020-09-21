using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.WebJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationToken = new CancellationTokenSource();
            try
            {
                var serviceProvider = Startup.RegisterServices();

                var services = serviceProvider.GetServices<IJobHandler>().ToList();

                await Console.Out.WriteLineAsync($"Alpaki web job is running! Service count: {services.Count()}");

                await Task.WhenAll(services.Select(s => s.WaitForMessages(cancellationToken.Token)));
            }
            finally
            {
                cancellationToken.Cancel();
                await Console.Out.WriteLineAsync("Alpaki web job is DONE!");
            }
        }
    }
}
