using System.Threading.Tasks;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using static Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions.SponsorsControllerExtensions;

namespace Alpaki.Tests.IntegrationTests.SponsorsTests
{
    public class AddSponsorTest : IntegrationTestsClass
    {
        public AddSponsorTest(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
        }

        [Fact]
        public async Task add_sponsor_is_successful()
        {
            IntegrationTestsFixture.SetUserVolunteerContext();
            var request = new AddSponsorRequest
            {
                Name = "H&M",
                Mail = "contact@handm.pl",
                ContactPerson = "Adam Kowalski",
                PhoneNumber = "123456789"
            };

            var sponsor = await Client.AddSponsor(request);
            
            sponsor.Should().NotBeNull();
            sponsor.SponsorId.Should().BePositive();
            
            var sponsorsResponse = await Client.GetSponsors();
            sponsorsResponse.Should().NotBeNull();
            sponsorsResponse.Sponsors.Should().NotBeNull().And.SatisfyRespectively(x =>
            {
                x.Id.Should().BePositive();
                x.Name.Should().Be(request.Name);
                x.Mail.Should().Be(request.Mail);
                x.ContactPerson.Should().Be(request.ContactPerson);
                x.PhoneNumber.Should().Be(request.PhoneNumber);
            });
        }

      
    }
}