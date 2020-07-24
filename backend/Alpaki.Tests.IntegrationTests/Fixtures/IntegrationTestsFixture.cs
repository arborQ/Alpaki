using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTestsFixture : IAsyncLifetime
    {
        public TestServer TestServer { get; }

        public IDatabaseContext DatabaseContext;

        public HttpClient ServerClient { get; }

        public IConfiguration Configuration { get; }

        public IntegrationTestsFixture()
        {
            Configuration = new IntegrationTestsConfigurationFixture().Configuration;
            var builder = new WebHostBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            DatabaseContext = TestServer.Services.GetService(typeof(IDatabaseContext)) as IDatabaseContext;
            ServerClient = TestServer.CreateClient();
        }

        public void SetUserCoordinatorContext()
        {
            SetUserContext(new User { Role = UserRoleEnum.Coordinator });
        }

        public void SetUserAdminContext()
        {
            SetUserContext(new User { Role = UserRoleEnum.Admin });
        }

        public void SetUserContext(User user)
        {
            var generator = TestServer.Services.GetService(typeof(IJwtGenerator)) as IJwtGenerator;
            var token = generator.Generate(user);

            ServerClient.DefaultRequestHeaders.Clear();
            ServerClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task InitializeAsync()
        {
            var connectionString = Configuration.GetValue<string>("DefaultConnectionString");
            await new Respawn.Checkpoint().Reset(connectionString);
        }

        public async Task DisposeAsync()
        {
            var connectionString = Configuration.GetValue<string>("DefaultConnectionString");
            await new Respawn.Checkpoint().Reset(connectionString);
        }
    }

}
