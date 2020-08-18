using System.Threading.Tasks;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.SponsorsTests
{
    public class RemoveSponsorTests : IntegrationTestsClass
    {
        public RemoveSponsorTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
        }

        [Fact]
        public async Task remove_sponsor()
        {
            IntegrationTestsFixture.SetUserVolunteerContext();

            var sponsor = await Client.AddSponsor(new SponsorsControllerExtensions.AddSponsorRequest
            {
                Name = "H&M",
                Mail = "contact@handm.pl",
                ContactPerson = "Adam Kowalski",
                PhoneNumber = "123456789"
            });
            

            var response = await Client.DeleteAsync($"/api/sponsors/{sponsor.SponsorId}");
            response.EnsureSuccessStatusCode();
            
            var sponsorsResponse = await Client.GetSponsors();
            sponsorsResponse.Should().NotBeNull();
            sponsorsResponse.Sponsors.Should().NotBeNull().And.BeEmpty();
        }
    }
}