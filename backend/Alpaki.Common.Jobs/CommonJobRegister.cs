using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Common.Jobs
{
    public static class CommonJobRegister
    {
        public static void Initialize(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var serviceBusSection = configuration.GetSection("ServiceBus");
            var connectionString = serviceBusSection.GetValue<string>("ConnectionString");
            var queueName = serviceBusSection.GetValue<string>("QueueName");

            serviceCollection.AddSingleton<IQueueClient>(_ => new QueueClient(connectionString, queueName));
            serviceCollection.AddSingleton<ITopicClient>(_ => new TopicClient(connectionString, queueName));

            serviceCollection.AddTransient<IJobPublisher, JobPublisher>();
            serviceCollection.AddTransient<IJobSubscriber, JobSubscriber>();
        }
    }
}
