using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alpaki.CrossCutting.Interfaces;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfig _configuration;
        private readonly ISystemClock _systemClock;
        private const int _validDays = 365;

        public JwtGenerator(IOptions<JwtConfig> configuration, ISystemClock systemClock)
        {
            _configuration = configuration.Value;
            _systemClock = systemClock;
        }

        public string Generate(IUser user)
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
                Expires = _systemClock.UtcNow.AddDays(_validDays).DateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}