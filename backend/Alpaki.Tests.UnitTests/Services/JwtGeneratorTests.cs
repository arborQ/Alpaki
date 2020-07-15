using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Services;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Services
{
    public class JwtGeneratorTests
    {
        private readonly Fixture _fixture;

        public JwtGeneratorTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(1, UserRoleEnum.Admin)]
        [InlineData(12, UserRoleEnum.Coordinator)]
        [InlineData(1222, UserRoleEnum.Volunteer)]
        public void JwtGenerator_Generate_ReturnsToken(long userId, UserRoleEnum role)
        {
            // Arrange
            var tokenHandler = new JwtSecurityTokenHandler();
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            var myConfiguration = new Dictionary<string, string>
                {
                    {"SeacretKey", key},
                };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var sut = new JwtGenerator(configuration);

            // Act
            var token = sut.Generate(userId, role);

            // Assert
            Assert.NotNull(token);
            var securityToken = tokenHandler.ReadJwtToken(token);
            Assert.Contains(securityToken.Claims, c => c.Type == "userId" && c.Value == userId.ToString());
            Assert.Contains(securityToken.Claims, c => c.Type == "role" && c.Value == ((int)role).ToString());
            //Assert.DoesNotContain(securityToken.Claims, c => c.Type == "exp");
        }
    }
}
