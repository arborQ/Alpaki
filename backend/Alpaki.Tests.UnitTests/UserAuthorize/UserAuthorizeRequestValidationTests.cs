using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alpaki.Logic.Services;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using System.Linq;
using AutoFixture;

namespace Alpaki.Tests.UnitTests.UserAuthorize
{
    public class UserAuthorizeRequestValidationTests
    {
        private readonly Fixture _fixture;

        public UserAuthorizeRequestValidationTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, "")]
        public async Task UserAuthorizeRequestValidation_ShouldReturnFail_IfInvalidRequest(string login, string password)
        {
            // Arrange
            var request = new UserAuthorizeRequest { Login = login, Password = password };
            var sut = new UserAuthorizeRequestValidation();

            // Act
            var result = await sut.ValidateAsync(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(UserAuthorizeRequest.Login));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(UserAuthorizeRequest.Password));
        }

        [Fact]
        public async Task UserAuthorizeRequestValidation_ShouldReturnSuccess_IfValidRequest()
        {
            // Arrange
            var request = _fixture.Create<UserAuthorizeRequest>();
            var sut = new UserAuthorizeRequestValidation();

            // Act
            var result = await sut.ValidateAsync(request);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(0, result.Errors.Count);
        }
    }
}
