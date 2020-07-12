using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.InvitationsTest
{
    public class InviteAVolunteerFake
    {
        public string Email { get; set; }
    }

    public class InvitationResponse
    {
        public InvitationItem[] Invitations { get; set; }
        public class InvitationItem
        {
            public int InvitationId { get; set; }
            public string Email { get; set; }
            public string Code { get; set; }
            public InvitationStateEnum Status { get; set; }
        }
    }
    public class InviteAVolunteerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly GraphQLClient _graphQL;
        private readonly Fixture _fixture;

        public InviteAVolunteerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _graphQL = new GraphQLClient(_client);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Basic_scenario()
        {
            var (fake,request )= _fixture
                .Build<InviteAVolunteerFake>()
                .With(
                    x => x.Email,
                    _fixture.Create<MailAddress>().Address
                )
                .Create()
                .WithJsonContent();

            var query = @"
query {
  invitations {
    invitationId
    email
    code
    status
  }
}
";
            var response = await _client.PostAsync("/api/invitations", request);

            response.EnsureSuccessStatusCode();
            var body = await response.ReadAs<InviteAVolunteerResponse>();
            body.InvitationId.Should().NotBe(0);
            
            var gqlResponse = await _graphQL.Query<InvitationResponse>(query);
            gqlResponse.Should().NotBeNull();
            gqlResponse.Invitations.Should().SatisfyRespectively(
                i =>
                {
                    i.Email.ToLowerInvariant().Should().BeEquivalentTo(fake.Email.ToLowerInvariant());
                    i.InvitationId.Should().NotBe(0);
                    i.Code.Should().NotBeNullOrWhiteSpace().And.MatchRegex("[0-9A-Z]{4}");
                    i.Status.Should().BeEquivalentTo(InvitationStateEnum.Pending);
                }
            );
            //TODO verify if event got published(mock IMediator to verify behaviour)
        }
    }
}