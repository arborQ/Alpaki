using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Alpaki.Moto.Job
{
    public abstract class QueueReader<TMessage> where TMessage : class
    {
        public readonly IQueueClient queueClient;
        protected QueueReader()
        {
            queueClient = new QueueClient("connectionString", "brands");
        }

        public async Task PingAsync(TMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            await queueClient.SendAsync(new Message(body));
        }

        public async Task WaitForMessages(CancellationToken token)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {

                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = true
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            token.WaitHandle.WaitOne();
            await queueClient.CloseAsync();
        }

        protected abstract Task<bool> HandleMessageProcess(TMessage messageBody, CancellationToken token);

        async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Process the message.
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            var body = JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(message.Body));

            await HandleMessageProcess(body, token);
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
