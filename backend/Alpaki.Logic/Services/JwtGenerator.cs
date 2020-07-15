using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Alpaki.CrossCutting.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string _secret;

        public JwtGenerator(IConfiguration configuration)
        {
            _secret = configuration.GetValue<string>("SeacretKey");
        }

        public string Generate(long userId, UserRoleEnum role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", userId.ToString()),
                    new Claim("role", ((int)role).ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
