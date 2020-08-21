using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alpaki.Tests.IntegrationTests
{
    public static class TestHelper
    {
        public static (T fake, StringContent json) WithJsonContent<T>(this T request) => (request, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        public static StringContent AsJsonContent<T>(this T request) => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");


        public static async Task<T> ReadAs<T>(this HttpResponseMessage response) => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        public static async Task<T> AsResponse<T>(this Task<HttpResponseMessage> responseTask)
        {
            var response = await responseTask;

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<ValidationProblemDetails> AsValidationResponse(this Task<HttpResponseMessage> responseTask)
        {
            var response = await responseTask;

            return JsonConvert.DeserializeObject<ValidationProblemDetails>(await response.Content.ReadAsStringAsync());
        }
    }
}