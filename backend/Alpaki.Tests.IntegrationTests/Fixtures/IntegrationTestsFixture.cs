using System.Net.Http;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Respawn;

namespace Alpaki.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTestsFixture
    {
        public TestServer TestServer { get; }

        public IDatabaseContext DatabaseContext;

        public HttpClient ServerClient { get; }

        public IConfiguration Configuration { get; }

        public string DatabaseConnectionString { get; private set; }

        public readonly static Checkpoint Checkpoint = new Checkpoint { TablesToIgnore = new[] { "__EFMigrationsHistory" } };

        public IntegrationTestsFixture()
        {
            Configuration = new IntegrationTestsConfigurationFixture().Configuration;
            var builder = new WebHostBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            DatabaseConnectionString = Configuration.GetValue<string>("DefaultConnectionString");
            DatabaseContext = TestServer.Services.GetService(typeof(IDatabaseContext)) as IDatabaseContext;
            ServerClient = TestServer.CreateClient();
        }

        public void SetUserVolunteerContext()
        {
            SetUserContext(new User { Role = UserRoleEnum.Volunteer });
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
    }
}
