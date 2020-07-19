using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alpaki.Database.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfig _configuration;

        public JwtGenerator(IOptions<JwtConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.SeacretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, ((int)user.Role).ToString()), 
                }),
                Expires = null,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}