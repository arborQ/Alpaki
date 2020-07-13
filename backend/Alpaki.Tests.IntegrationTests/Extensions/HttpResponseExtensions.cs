using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alpaki.Tests.IntegrationTests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> GetResponse<T>(this HttpResponseMessage message)
        {
            var stringResponse = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
