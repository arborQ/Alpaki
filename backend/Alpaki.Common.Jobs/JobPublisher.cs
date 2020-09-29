using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Common.Jobs.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Alpaki.Common.Jobs
{
    internal class JobPublisher : IJobPublisher
    {
        private readonly ITopicClient _queueClient;
        private readonly SemaphoreSlim Semaphore = new SemaphoreSlim(10);

        public JobPublisher(ITopicClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task PublishBusinesEvent<T>(T eventMessage, Guid eventId)
        {
            var @event = new BusinessEventModel<T>
            {
                EventId = eventId,
                EventType = typeof(T).FullName,
                Message = eventMessage
            };

            var messageString = JsonConvert.SerializeObject(@event);

            await Semaphore.WaitAsync();
            await _queueClient
                .SendAsync(new Message(Encoding.UTF8.GetBytes(messageString)))
                .ContinueWith(_ => Semaphore.Release());
        }
    }
}
