using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using static Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions.SponsorsControllerExtensions;

namespace Alpaki.Tests.IntegrationTests.SponsorsTests
{
    public class GetSponsorTest : IntegrationTestsClass
    {
        public GetSponsorTest(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
        }

        [Fact]
        public async Task get_sponsor_is_successful()
        {
            IntegrationTestsFixture.SetUserVolunteerContext();
            var request = new AddSponsorRequest
            {
                Name = "H&M",
                Mail = "contact@handm.pl",
                ContactPerson = "Adam Kowalski",
                PhoneNumber = "123456789",
                Brand = "Kraków",
                CooperationType = SponsorCooperationEnum.Permanent
            };
            var sponsor = await Client.AddSponsor(request);
            
            var sponsorResponse = await Client.GetSponsor(sponsor.SponsorId);
            
            sponsorResponse.Should().NotBeNull();
            sponsorResponse.SponsorId.Should().Be(sponsor.SponsorId);
            sponsorResponse.Name.Should().Be(request.Name);
            sponsorResponse.Mail.Should().Be(request.Mail);
            sponsorResponse.ContactPerson.Should().Be(request.ContactPerson);
            sponsorResponse.PhoneNumber.Should().Be(request.PhoneNumber);
            sponsorResponse.Brand.Should().Be(request.Brand);
            sponsorResponse.CooperationType.Should().Be(request.CooperationType);
        }
    }
}