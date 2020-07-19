using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.Fixtures
{
    [Collection("IntegrationTests")]
    public class IntegrationTestsClass : IClassFixture<IntegrationTestsFixture>, IAsyncLifetime
    {
        public IntegrationTestsClass(IntegrationTestsFixture integrationTestsFixture)
        {
            IntegrationTestsFixture = integrationTestsFixture;
            Client = integrationTestsFixture.ServerClient;
        }

        protected IntegrationTestsFixture IntegrationTestsFixture { get; }

        protected HttpClient Client { get; }

        public Task DisposeAsync()
        {
            return IntegrationTestsFixture.DisposeAsync();
        }

        public Task InitializeAsync()
        {
            return IntegrationTestsFixture.InitializeAsync();
        }
    }

}
