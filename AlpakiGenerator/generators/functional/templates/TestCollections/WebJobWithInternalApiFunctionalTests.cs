using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pyra.Common.ServiceBus;
using Pyra.Csp.LicenseAssignments.Infrastructure.Clients.PartnerNetwork.Partner.Factory;
using Pyra.Csp.LicenseAssignments.Tests.Integration.Fixtures;
using Pyra.Csp.LicenseAssignments.Tests.Integration.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Pyra.Csp.LicenseAssignments.Tests.Functional.TestCollections
{
    public class WebJobFixture : IntegrationTestsConfigurationFixture, IAsyncLifetime
    {
        private WebJobTestHost _webJobTestHost;
        public InternalApiIntegrationTestsFixture InternalApiIntegrationTestsFixture { get; set; }
        public ServiceBusFixture ServiceBusFixture { get; set; }

        public SqlConnection SqlConnection { get; private set; }

        public WebJobFixture(IMessageSink messageSink)
            : base(messageSink)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            InternalApiIntegrationTestsFixture = new InternalApiIntegrationTestsFixture(this);

            ServiceDiscoveryRegistration.CleanRegisteredServiceFabricServices();
            ServiceDiscoveryRegistration.RegisterFakeServiceEndpoints(InternalApiIntegrationTestsFixture.FakeServer.Ports.First());

            ServiceBusFixture = new ServiceBusFixture(this);
            ServiceBusFixture.Initialize();

            _webJobTestHost = new WebJobTestHost(
                Logger,
                services =>
                {
                    services.AddSingleton(ServiceBusFixture);
                    services.Replace(new ServiceDescriptor(typeof(IBusSubscriber<>), typeof(TopicSubscriberSelfDestroyingTopicWrapper<>), ServiceLifetime.Singleton));
                    services.Replace(new ServiceDescriptor(typeof(IPartnerClientFactory), new FakePartnerClientFactory()));
                }
                );
            _webJobTestHost.Start(cancellationTokenSource.Token);
            SqlConnection = _webJobTestHost.ServiceProvider.GetService<SqlConnection>();

            InternalApiIntegrationTestsFixture.SqlFixture.InitializeAsync().GetAwaiter().GetResult();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await Task.WhenAll(TopicTracker.UsedTopics.Select(s => ServiceBusFixture.ServiceBusManagementClient.DeleteTopicAsync(s)));

            TopicTracker.UsedTopics.Clear();
        }
    }

    [CollectionDefinition(nameof(WebJobWithInternalApiFunctionalTests))]
    public class WebJobWithInternalApiFunctionalTests :
        ICollectionFixture<WebJobFixture>
    {
    }
}