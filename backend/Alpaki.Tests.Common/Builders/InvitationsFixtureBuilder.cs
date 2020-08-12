using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models.Invitations;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.IntegrationTests.Fixtures.Builders
{
    public static class InvitationsFixtureBuilder
    {
        public static IPostprocessComposer<Invitation> UseInvitation(this Fixture fixture)
        {
            return fixture.Build<Invitation>().Without(i => i.InvitationId);
        }

        public static IPostprocessComposer<Invitation> WithStatus(this IPostprocessComposer<Invitation> postProcessComposer, InvitationStateEnum status)
        {
            return postProcessComposer.With(i => i.Status, status);
        }
    }
}
