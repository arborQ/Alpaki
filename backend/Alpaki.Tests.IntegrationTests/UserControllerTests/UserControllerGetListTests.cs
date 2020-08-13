using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Handlers.GetUser;
using Alpaki.Logic.Handlers.GetUsers;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.UserControllerTests
{
    public class UserControllerGetListTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public UserControllerGetListTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }


        [Theory]
        [InlineData(UserRoleEnum.Admin, true)]
        [InlineData(UserRoleEnum.Coordinator, true)]
        [InlineData(UserRoleEnum.Volunteer, true)]
        [InlineData(UserRoleEnum.None, false)]
        public async Task UserControllerGet_CheckRoles(UserRoleEnum role, bool canAccess)
        {
            // Arrange
            IntegrationTestsFixture.SetUserContext(new Database.Models.User { Role = role });

            // Act
            var response = await Client.GetAsync($"/api/user");

            // Assert
            if (canAccess)
            {
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            else
            {
                response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            }
        }

        [Theory]
        [InlineData(10, 1, 10)]
        [InlineData(20, 1, 10)]
        [InlineData(25, 3, 5)]
        [InlineData(25, null, 25)]
        public async Task UserControllerGet_Works(int userCount, int? page, int expectedCount)
        {
            // Arrange
            var users = _fixture.VolunteerBuilder().CreateMany(userCount);

            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(users);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserAdminContext();

            // Act
            var response = await Client.GetAsync($"/api/user?page={page}").AsResponse<GetUsersResponse>();

            // Assert
            response.Users.Should().HaveCount(expectedCount);
        }

        [Fact]
        public async Task UserController_Get_Details()
        {
            // Arrange
            var users = _fixture.VolunteerBuilder().CreateMany(8);

            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(users);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserAdminContext();

            // Act
            var responses = await Task.WhenAll(users.Select(u => Client.GetAsync($"/api/user/details?userId={u.UserId}").AsResponse<GetUserResponse>()));

            // Assert
            foreach (var user in users)
            {
                var resultUser = responses.SingleOrDefault(u => u.UserId == user.UserId);
                resultUser.Should().NotBeNull();
                resultUser.FirstName.Should().Be(user.FirstName);

            }
        }
    }
}
