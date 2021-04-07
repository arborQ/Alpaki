using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SyncPartyShopSearchIndex.Functions
{
    public class AuthorizeFunction
    {
        [FunctionName("ValidateUser")]
        public async Task<IActionResult> ValidateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            using var streamReader = new StreamReader(req.Body);
            var requestBody = await streamReader.ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<LoginModel>(requestBody);

            return new OkObjectResult(data);
        }
    }

    class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
