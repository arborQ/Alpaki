﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alpaki.Tests.IntegrationTests
{
    public static class TestHelper
    {
        public static (T fake, StringContent json) WithJsonContent<T>(this T request) => (request, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        public static async Task<T> ReadAs<T>(this HttpResponseMessage response) => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}