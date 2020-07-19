using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alpaki.Tests.IntegrationTests
{
    public class GraphQLClient
    {
        public class GraphResponse<T>
        {
            public T Data { get; set; }
        }

        private readonly HttpClient _httpClient;

        public GraphQLClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Query<T>(string query)
        {
            var response = await this._httpClient.GetAsync($"/gl?query={query}");

            var stringResponse = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<GraphResponse<T>>(stringResponse);

            return items.Data;
        }
    }
}
