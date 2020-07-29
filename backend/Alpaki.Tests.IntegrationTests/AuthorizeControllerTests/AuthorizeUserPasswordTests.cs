using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AuthorizeUserPassword;
using Alpaki.Tests.IntegrationTests.Extensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.AuthorizeControllerTests
{
    public class AuthorizeUserPasswordTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public AuthorizeUserPasswordTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("password")]
        [InlineData("password1234!!!!###")]
        [InlineData("!@#$%^&*()_+=-0987654321ŻółXCĆŚźś")]
        public async Task AuthorizeUserPassword_ReturnsToken_IfValidPassword(string password)
        {
            // Arrange
            var user = _fixture.VolunteerBuilder().WithPassword(password).Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await Client.PostAsync("/api/Authorize", new { login = user.Email, password }.WithJsonContent().json);
            var responseBody = await response.GetResponse<AuthorizeUserPasswordResponse>();

            // Arrange
            response.EnsureSuccessStatusCode();
            responseBody.Token.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("arbor123@O2.PL", "Arbor123@o2.pl", "password")]
        [InlineData("ARBOR@gmail.com", "arbor@gmail.com", "password1234!!!!###")]
        [InlineData("ArBoRRR!@tessssXtttt.com", "ArBoRRR!@tessssxtttt.COM", "!@#$%^&*()_+=-0987654321ŻółXCĆŚźś")]
        [InlineData("AAAAAA@BBBBBB.COM", "aaaaaa@bbbbbb.com", "!@#$%^&*()_+=-0987654321ŻółXCĆŚźś")]
        public async Task AuthorizeUserPassword_ReturnsToken_IfValidPassword_CaseInsensitive(string login, string differentCaseLogin, string password)
        {
            // Arrange
            var user = _fixture.VolunteerBuilder().With(u => u.Email, login).WithPassword(password).Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await Client.PostAsync("/api/Authorize", new { login = differentCaseLogin, password }.WithJsonContent().json);
            var responseBody = await response.GetResponse<AuthorizeUserPasswordResponse>();

            // Arrange
            response.EnsureSuccessStatusCode();
            responseBody.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task AuthorizeUserPassword_ReturnsBadRequest_IfInvalidValidPassword()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var invalidPassword = _fixture.Create<string>();

            var user = _fixture.VolunteerBuilder().WithPassword(password).Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await IntegrationTestsFixture.ServerClient.PostAsync("/api/authorize", new { login = user.Email, password = invalidPassword }.WithJsonContent().json);
            var responseBody = await response.GetResponse<AuthorizeUserPasswordResponse>();

            // Arrange
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Token.Should().BeNull();
        }

        [Fact]
        public async Task AuthorizeUserPassword_ReturnsBadRequest_IfInvalidUserName()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);

            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(users);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await Client.PostAsync("/api/Authorize", new { login = _fixture.Create<string>(), password }.WithJsonContent().json);
            var responseBody = await response.GetResponse<AuthorizeUserPasswordResponse>();

            // Arrange
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Token.Should().BeNull();
        }
    }
}
