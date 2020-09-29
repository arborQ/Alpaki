using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Common.Jobs.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Alpaki.Common.Jobs
{
    internal class JobSubscriber : IJobSubscriber
    {
        private readonly IQueueClient _queueClient;

        public JobSubscriber(IQueueClient queueClient)
        {
            _queueClient = queueClient;
            Console.WriteLine("INITIALIZE JobSubscriber");
        }

        public async Task Subscribe(Func<StringBusinessEventModel, Task> eventAction, string eventType, CancellationToken cancellationToken)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1000,
                AutoComplete = false,
            };
            _queueClient.RegisterMessageHandler(async (message, token) =>
            {

                var messageBody = GetMessage(message);
                if (messageBody.EventType == eventType)
                {
                    await eventAction(messageBody);
                    await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Unhandled event type [{messageBody.EventType}]");
                }
            }, messageHandlerOptions);

            cancellationToken.WaitHandle.WaitOne();
            await _queueClient.CloseAsync();
        }

        StringBusinessEventModel GetMessage(Message message)
        {
            var body = JsonConvert.DeserializeObject<StringBusinessEventModel>(Encoding.UTF8.GetString(message.Body));

            return body;
        }

        async Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            await Console.Out.WriteLineAsync($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            await Console.Out.WriteLineAsync("Exception context for troubleshooting:");
            await Console.Out.WriteLineAsync($"- Endpoint: {context.Endpoint}");
            await Console.Out.WriteLineAsync($"- Entity Path: {context.EntityPath}");
            await Console.Out.WriteLineAsync($"- Executing Action: {context.Action}");
        }
    }
}
