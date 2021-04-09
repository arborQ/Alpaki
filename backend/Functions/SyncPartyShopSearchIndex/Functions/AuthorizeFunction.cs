using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JWT.Algorithms;
using JWT.Builder;
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
        private readonly AuthorizeConfig _authorizeConfig;

        public AuthorizeFunction(AuthorizeConfig authorizeConfig)
        {
            _authorizeConfig = authorizeConfig;
        }

        [FunctionName("ValidateUser")]
        public async Task<IActionResult> ValidateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            using var streamReader = new StreamReader(req.Body);
            var requestBody = await streamReader.ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<LoginModel>(requestBody);

            if (data.Password != "test")
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(new
            {
                Token = GenerateAccessToken(data.Login, 1),
                SeacretLength = _authorizeConfig.SeacretKey?.Length
            });
        }

        string GenerateAccessToken(string login, int role)
        {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Encoding.ASCII.GetBytes(_authorizeConfig.SeacretKey))
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                .AddClaim("username", login)
                .AddClaim("role", role)
                .Encode();
        }
    }

    class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
