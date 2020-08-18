using System.Threading.Tasks;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using static Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions.SponsorsControllerExtensions;

namespace Alpaki.Tests.IntegrationTests.SponsorsTests
{
    public class UpdateSponsorTests : IntegrationTestsClass
    {
        public UpdateSponsorTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
        }

        [Fact]
        public async Task update_sponsor()
        {
            IntegrationTestsFixture.SetUserVolunteerContext();

            var sponsor = await Client.AddSponsor(new AddSponsorRequest
            {
                Name = "H&M",
                Mail = "contact@handm.pl",
                ContactPerson = "Adam Kowalski",
                PhoneNumber = "123456789"
            });
            
            var request = new UpdateSponsorRequest
            {
                Id = sponsor.SponsorId,
                Name = "H&M2",
                Mail = "contact2@handm.pl",
                ContactPerson = "Adam Kowalski2",
                PhoneNumber = "1234567892"
            };
            var response = await Client.PutAsync("/api/sponsors", request.WithJsonContent().json);
            response.EnsureSuccessStatusCode();
            
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

        private class UpdateSponsorRequest
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string PhoneNumber { get; set; }
            public string Mail { get; set; }
        }
    }
}