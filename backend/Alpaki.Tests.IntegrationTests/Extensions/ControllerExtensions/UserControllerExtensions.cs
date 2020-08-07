using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.GetUsers;

namespace Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions
{
    static class UserControllerExtensions
    {
        public static Task<GetUsersResponse> GetUsers(this HttpClient client)
        {
            return client.GetAsync("/api/user").AsResponse<GetUsersResponse>();
        }

        public static Task<GetUsersResponse> GetUsers(this HttpClient client, long dreamId)
        {
            return client.GetAsync($"/api/user?dreamId={dreamId}").AsResponse<GetUsersResponse>();
        }
    }
}
