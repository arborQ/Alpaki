using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.GetInvitations;

namespace Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions
{
    static class InvitationsControllerExtensions
    {
        public static Task<GetInvitationsResponse> GetInvitations(this HttpClient client)
        {
            return client.GetAsync("/api/invitations").AsResponse<GetInvitationsResponse>();
        }

    }
}
