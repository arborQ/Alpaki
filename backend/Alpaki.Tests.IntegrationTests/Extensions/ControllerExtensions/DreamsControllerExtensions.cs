using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Handlers.GetDreams;

namespace Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions
{
    static class DreamsControllerExtensions
    {
        public static Task<GetDreamsResponse> GetDreams(this HttpClient client, int? ageFrom = null, int? ageTo = null, DreamStateEnum? status = null, long[] categories = null)
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

            if (status.HasValue)
            {
                queryString.Add("status", ((int)status.Value).ToString());
            }

            if (categories != null)
            {
                foreach(var category in categories)
                {
                    queryString.Add("categories", category.ToString());
                }
            }

            return client.GetAsync($"/api/dreamers?{queryString}").AsResponse<GetDreamsResponse>();
        }
    }
}
