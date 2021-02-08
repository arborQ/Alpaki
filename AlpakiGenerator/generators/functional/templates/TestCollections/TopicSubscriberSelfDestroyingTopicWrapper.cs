using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pyra.Common.ServiceBus;
using Pyra.Common.ServiceBus.Configuration;
using Pyra.Common.ServiceBus.Handlers;
using Pyra.Common.ServiceBus.Messages;
using Pyra.Common.ServiceBus.Naming;
using Pyra.Foundation.AppInsights.Common.RequestsCorrelation;
using <%= name %>.Infrastructure.Bus.ServiceBus;
using Serilog;

namespace <%= name %>.Tests.Functional
{
    public class TopicTracker
    {
        public static ConcurrentBag<string> UsedTopics = new ConcurrentBag<string>();
    }

    public class TopicSubscriberSelfDestroyingTopicWrapper<TMessage> : TopicTracker, IBusSubscriber<TMessage>
    {
        private readonly IBusSubscriber<TMessage> _busSubscriber;
        private readonly string _topicName;
        
        public TopicSubscriberSelfDestroyingTopicWrapper(BusManager busManager,
            SubscriberSettings<TMessage> subscriberSettings,
            ITopicNameGetter topicNameGetter,
            IMessageSerializer<TMessage> messageSerializer,
            ILogger logger,
            TrackingModule trackingModule)
        {
            _topicName = topicNameGetter.Get<TMessage>(subscriberSettings);

            _busSubscriber = new TopicSubscriberV2<TMessage>(
                busManager,
                subscriberSettings,
                topicNameGetter,
                messageSerializer,
                logger,
                trackingModule);

            UsedTopics.Add(_topicName);
        }

        public void Dispose()
        {
            _busSubscriber.Dispose();
        }

        public Task Subscribe(Func<TMessage, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
        {
            return _busSubscriber.Subscribe(messageHandler, cancellationToken);
        }

        public Task Subscribe(IMessageHandler<TMessage> messageHandler, CancellationToken cancellationToken)
        {
            return _busSubscriber.Subscribe(messageHandler, cancellationToken);
        }

        public Task Subscribe(Func<IMessageHandler<TMessage>> handlerFactory, CancellationToken cancellationToken)
        {
            return _busSubscriber.Subscribe(handlerFactory, cancellationToken);
        }

        public Task Subscribe(Func<IEnumerable<IMessageHandler<TMessage>>> handlerFactory, CancellationToken cancellationToken)
        {
            return _busSubscriber.Subscribe(handlerFactory, cancellationToken);
        }
    }
}