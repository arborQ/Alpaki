using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Services;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Services
{
    public class JwtGeneratorTests
    {
        private readonly Fixture _fixture;
        private readonly ISystemClock _systemClock;

        public JwtGeneratorTests()
        {
            _fixture = new Fixture();
            _systemClock = Substitute.For<ISystemClock>();
            _systemClock.UtcNow.Returns(DateTime.UtcNow);
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

            var configuration = Substitute.For<IOptions<JwtConfig>>();
            configuration.Value.Returns(new JwtConfig { SeacretKey = key });

            var sut = new JwtGenerator(configuration, _systemClock);

            // Act
            var token = sut.Generate(new User { UserId = userId, Role = role });

            // Assert
            Assert.NotNull(token);
            var securityToken = tokenHandler.ReadJwtToken(token);
            Assert.Contains(securityToken.Claims, c => c.Type == "unique_name" && c.Value == userId.ToString());
            Assert.Contains(securityToken.Claims, c => c.Type == "role" && c.Value == ((int)role).ToString());
            //Assert.DoesNotContain(securityToken.Claims, c => c.Type == "exp");
        }
    }
}
