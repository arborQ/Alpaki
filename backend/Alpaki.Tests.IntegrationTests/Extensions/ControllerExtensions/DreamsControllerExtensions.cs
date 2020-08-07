using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Handlers.GetDreams;

namespace Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions
{
    static class DreamsControllerExtensions
    {
        public static Task<GetDreamsResponse> GetDreams(this HttpClient client, int? ageFrom = null, int? ageTo = null, GenderEnum? gender = null, DreamStateEnum? status = null, long[] categories = null)
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (ageFrom.HasValue)
            {
                queryString.Add("ageFrom", ageFrom.Value.ToString());
            }

            if (ageTo.HasValue)
            {
                queryString.Add("ageTo", ageTo.Value.ToString());
            }

            if (gender.HasValue)
            {
                queryString.Add("gender", gender.Value.ToString());
            }

            if (status.HasValue)
            {
                queryString.Add("status", status.Value.ToString());
            }

            if (categories != null)
            {
                queryString.Add("categories", string.Join(",", categories));
            }

            return client.GetAsync($"/api/dreams?{queryString}").AsResponse<GetDreamsResponse>();
        }
    }
}
