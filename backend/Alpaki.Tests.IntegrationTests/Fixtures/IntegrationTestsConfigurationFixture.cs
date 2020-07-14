using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTestsConfigurationFixture : IAsyncLifetime
    {
        public IConfiguration Configuration { get; private set; }

        public IntegrationTestsConfigurationFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ", "Test");
            var defaultConfigurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Test.json")
                .AddJsonFile("appsettings.TestLocal.json", true)
                .Build();

            Configuration = defaultConfigurationBuilder;
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
