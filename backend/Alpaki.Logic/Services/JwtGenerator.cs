using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alpaki.Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string _secret;

        public JwtGenerator(string secret)
        {
            _secret = secret;
        }

        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()), 
                }),
                Expires = null,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}