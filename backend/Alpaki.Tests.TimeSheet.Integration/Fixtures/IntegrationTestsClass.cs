using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Alpaki.Tests.TimeSheet.Integration.Fixtures
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

        public async Task DisposeAsync()
        {
            await IntegrationTestsFixture.Checkpoint.Reset(IntegrationTestsFixture.DatabaseConnectionString);
        }

        public async Task InitializeAsync()
        {
            await IntegrationTestsFixture.Checkpoint.Reset(IntegrationTestsFixture.DatabaseConnectionString);
        }
    }

}
