using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using Xunit;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;

namespace Alpaki.Tests.IntegrationTests.UserControllerTests
{
    public class UserControllerDeleteTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public UserControllerDeleteTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        private Task<HttpResponseMessage> DeleteUserAction(long userId)
        {
            var responseTask = Client.DeleteAsync($"/api/User?userId={userId}");

            return responseTask;
        }

        [Theory]
        [InlineData(UserRoleEnum.Coordinator)]
        [InlineData(UserRoleEnum.Volunteer)]
        [InlineData(UserRoleEnum.None)]
        public async Task UserControllerDelete_IsForbidden_IfNotAdmin(UserRoleEnum role)
        {
            // Arrange
            IntegrationTestsFixture.SetUserContext(new User { UserId = 10, Role = role });

            var user = _fixture.VolunteerBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await DeleteUserAction(user.UserId);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task UserControllerDelete_OnlyAdmin_CanAccess()
        {
            // Arrange
            IntegrationTestsFixture.SetUserAdminContext();

            var user = _fixture.VolunteerBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await DeleteUserAction(user.UserId);

            // Assert
            response.EnsureSuccessStatusCode();
            var userExists = IntegrationTestsFixture.DatabaseContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
            Assert.Null(userExists);
        }

        [Fact]
        public async Task UserControllerDelete_CanRemove_WithAssignedDreams()
        {
            // Arrange
            IntegrationTestsFixture.SetUserAdminContext();

            var user = _fixture.VolunteerBuilder().Create();
            var dreams = _fixture.DreamBuilder().WithNewCategory().CreateMany(10);
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(dreams.Select(d => new AssignedDreams { Dream = d, Volunteer = user }));
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await DeleteUserAction(user.UserId);

            // Assert
            response.EnsureSuccessStatusCode();
            var userExists = IntegrationTestsFixture.DatabaseContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
            Assert.Null(userExists);
        }

        [Fact]
        public async Task UserControllerDelete_CantDelete_Self()
        {
            // Arrange

            var user = _fixture.AdminBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            IntegrationTestsFixture.SetUserContext(user);

            // Act
            var response = await DeleteUserAction(user.UserId);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
