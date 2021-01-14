using System.Net.Http;
using Alpaki.TimeSheet.Database;
using Alpaki.WebApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Respawn;
using Microsoft.AspNetCore.TestHost;
using MediatR;

namespace Alpaki.Tests.TimeSheet.Integration.Fixtures
{
    public class IntegrationTestsFixture
    {
        public ITimeSheetDatabaseContext DatabaseContext;

        public HttpClient ServerClient { get; }

        public IConfiguration Configuration { get; }
        public IMediator Mediator { get; }

        public string DatabaseConnectionString { get; private set; }

        public readonly static Checkpoint Checkpoint = new Checkpoint { TablesToIgnore = new[] { "TimeSheetMigrationsHistory" } };

        public IntegrationTestsFixture()
        {
            Configuration = new IntegrationTestsConfigurationFixture().Configuration;
            var builder = new WebHostBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();
            var testServer = new TestServer(builder);

            DatabaseConnectionString = Configuration.GetValue<string>("DefaultConnectionString");
            DatabaseContext = testServer.Services.GetService(typeof(ITimeSheetDatabaseContext)) as ITimeSheetDatabaseContext;
            Mediator = testServer.Services.GetService(typeof(IMediator)) as IMediator;
        }
    }
}
