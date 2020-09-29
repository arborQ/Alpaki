using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Common.Jobs;
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

                var subscriberA = serviceProvider.GetService<IJobSubscriber>();
                var subscriberB = serviceProvider.GetService<IJobSubscriber>();
                var publisher = serviceProvider.GetService<IJobPublisher>();

                Task.Run(() =>
                {
                    subscriberA.Subscribe(async message =>
                    {
                        await Console.Out.WriteLineAsync($"A Event [{message.Message}]");
                    }, "System.String", cancellationToken.Token);
                });

                Task.Run(() =>
                {
                    subscriberB.Subscribe(async message =>
                    {
                        await Console.Out.WriteLineAsync($"B Event [{message.Message}]");
                    }, "System.String", cancellationToken.Token);
                });

                while (true)
                {
                    await Console.Out.WriteLineAsync("Type message");
                    var message = await Console.In.ReadLineAsync();

                    if (message == "end")
                    {
                        return;
                    }

                    await Console.Out.WriteLineAsync($"Send message: [{message}]");

                    await publisher.PublishBusinesEvent($"[{message}]", Guid.NewGuid());
                }
                //await Console.Out.WriteLineAsync($"Alpaki web job is running! Service count: {services.Count()}");

                //await Task.WhenAll(services.Select(s => s.WaitForMessages(cancellationToken.Token)));
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            finally
            {
                cancellationToken.Cancel();
                await Console.Out.WriteLineAsync("Alpaki web job is DONE!");
            }
        }
    }
}
