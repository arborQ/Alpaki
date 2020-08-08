using System.Threading.Tasks;
using Alpaki.Logic.Handlers.GetInvitations;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.InvitationsTest
{
    public class InvitationsControllerTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public InvitationsControllerTests(IntegrationTestsFixture integrationTestsFixture) 
            : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task InvitationsController_GetInvitations_OnlyPending()
        {
            // Arrange
            IntegrationTestsFixture.SetUserCoordinatorContext();
            var invitations = _fixture.UseInvitation().WithStatus(CrossCutting.Enums.InvitationStateEnum.Pending).CreateMany(20);
            var invitationsAccepted = _fixture.UseInvitation().WithStatus(CrossCutting.Enums.InvitationStateEnum.Accepted).CreateMany(10);
            await IntegrationTestsFixture.DatabaseContext.Invitations.AddRangeAsync(invitations);
            await IntegrationTestsFixture.DatabaseContext.Invitations.AddRangeAsync(invitationsAccepted);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var result = await Client.GetAsync("/api/invitations").AsResponse<GetInvitationsResponse>();

            // Assert
            result.Invitations.Should().HaveCount(20);
        }
    }
}
