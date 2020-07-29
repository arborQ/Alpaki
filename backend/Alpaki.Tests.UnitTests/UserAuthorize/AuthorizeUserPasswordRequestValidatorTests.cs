using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Logic.Handlers.AuthorizeUserPassword;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.UserAuthorize
{
    public class AuthorizeUserPasswordRequestValidatorTests
    {
        private readonly Fixture _fixture;
        private readonly AuthorizeUserPasswordRequestValidator _sut;
        private readonly IDatabaseContext _databaseContext;

        public AuthorizeUserPasswordRequestValidatorTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();
            _sut = new AuthorizeUserPasswordRequestValidator(_databaseContext);
        }

        [Fact]
        public async Task AuthorizeUserPasswordHandler_ReturnOk_IfUserExists()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);
            var usersList = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            var usedUser = users.First();

            // Act
            var result = await _sut.ValidateAsync(new AuthorizeUserPasswordRequest { Login = usedUser.Email });

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task AuthorizeUserPasswordHandler_ReturnError_IfUserNotExists()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);
            var usersList = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            var usedUser = users.First();

            // Act
            var result = await _sut.ValidateAsync(new AuthorizeUserPasswordRequest { Login = _fixture.Create<string>() });

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AuthorizeUserPasswordRequest.Login));
        }
    }
}
