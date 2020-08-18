using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Alpaki.Tests.IntegrationTests.UserControllerTests
{
    public class UserControllerChangeRoleTests : IntegrationTestsClass
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Fixture _fixture;
        public UserControllerChangeRoleTests(IntegrationTestsFixture integrationTestsFixture, ITestOutputHelper testOutputHelper) : base(integrationTestsFixture)
        {
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(UserRoleEnum.Volunteer,UserRoleEnum.Coordinator)]
        [InlineData(UserRoleEnum.Volunteer,UserRoleEnum.Admin)]
        [InlineData(UserRoleEnum.Coordinator, UserRoleEnum.Admin)]
        public async Task admin_can_change_user_role_from_to(UserRoleEnum from, UserRoleEnum to)
        {
            IntegrationTestsFixture.SetUserAdminContext();

            var user = _fixture.UserBuilder()
                .With(x => x.Role, from)
                .Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var response = await ChangeRole(new { userId = user.UserId, role = (int)to });

            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync()); ;
            response.EnsureSuccessStatusCode();

            var inDb = await IntegrationTestsFixture.DatabaseContext.Users.AsNoTracking().SingleAsync(x => x.UserId == user.UserId);
            inDb.Role.Should().Be(to);
        }

        [Theory]
        [InlineData(UserRoleEnum.Volunteer)]
        [InlineData(UserRoleEnum.Coordinator)]
        [InlineData(UserRoleEnum.None)]
        public async Task admin_cannot_change_role_of_another_admin(UserRoleEnum toRole)
        {
            IntegrationTestsFixture.SetUserAdminContext();
            var admin = _fixture.AdminBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(admin);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var response = await ChangeRole(new { userId = admin.UserId, role = (int)toRole });

            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync()); ;
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(UserRoleEnum.Coordinator)]
        [InlineData(UserRoleEnum.Volunteer)]
        [InlineData(UserRoleEnum.None)]
        public async Task only_admin_can_change_role(UserRoleEnum role)
        {
            var contextUser = _fixture.UserBuilder().With(x => x.Role, role)
                .Create();

            IntegrationTestsFixture.SetUserContext(contextUser);

            var user = _fixture.UserBuilder()
                .With(x => x.Role, UserRoleEnum.Volunteer)
                .Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var response = await ChangeRole(new {userId = user.UserId, role = (int) UserRoleEnum.Admin});

            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync()); ;
            response.IsSuccessStatusCode.Should().BeFalse();
        }

        private async Task<HttpResponseMessage> ChangeRole<T>(T request)
        {
            var (_, data) = request.WithJsonContent();

            return await Client.PatchAsync("api/user/role", data);
        }
    }
}