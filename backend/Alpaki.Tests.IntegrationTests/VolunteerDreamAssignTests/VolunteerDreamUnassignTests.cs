using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.Extensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.VolunteerDreamAssignTests
{
    public class VolunteerDreamUnassignTests : IntegrationTestsClass
    {
        private const string ApiUrlPath = "/api/Volunteers/assign";
        private readonly Fixture _fixture;

        private static string CreateDeleteUrl(long userId, long dreamId)
        {
            return $"{ApiUrlPath}?volunteerId={userId}&dreamId={dreamId}";
        }

        public VolunteerDreamUnassignTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Theory(DisplayName = "VolunteerDreamUnassign coordinator and admin can remove volonteer assing")]
        [InlineData(UserRoleEnum.Admin)]
        [InlineData(UserRoleEnum.Coordinator)]
        public async Task VolunteersController_Delete_RemoveVolunteerAssign(UserRoleEnum role)
        {
            // Arrange
            IntegrationTestsFixture.SetUserContext(new User { Role = role });
            var dream = _fixture.DreamBuilder().WithNewCategory().Create();
            var user = _fixture.VolunteerBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddAsync(new AssignedDreams { Dream = dream, Volunteer = user });
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await Client.DeleteAsync(CreateDeleteUrl(user.UserId, dream.DreamId));
            var existingAssign = await IntegrationTestsFixture.DatabaseContext.AssignedDreams.FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            existingAssign.Should().BeNull();
        }

        [Theory(DisplayName = "VolunteerDreamUnassign coordinator and admin only can remove volonteer assing")]
        [InlineData(UserRoleEnum.Volunteer)]
        [InlineData(UserRoleEnum.None)]
        public async Task VolunteersController_CantDelete_RemoveVolunteerAssign(UserRoleEnum role)
        {
            // Arrange
            IntegrationTestsFixture.SetUserContext(new User { Role = role });
            var dream = _fixture.DreamBuilder().WithNewCategory().Create();
            var user = _fixture.VolunteerBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddAsync(new AssignedDreams { Dream = dream, Volunteer = user });
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await Client.DeleteAsync(CreateDeleteUrl(user.UserId, dream.DreamId));
            var existingAssign = await IntegrationTestsFixture.DatabaseContext.AssignedDreams.FirstOrDefaultAsync();

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            existingAssign.Should().NotBeNull();
        }

        [Fact(DisplayName = "VolunteerDreamUnassign fail if there is no assign")]
        public async Task VolunteersController_Delete_FailIfNoAssign()
        {
            // Arrange
            IntegrationTestsFixture.SetUserCoordinatorContext();
            var userId = _fixture.Create<long>();
            var dreamId = _fixture.Create<long>();
            // Act
            var response = await Client.DeleteAsync(CreateDeleteUrl(userId, dreamId));
            var responseObject = await response.GetResponse<ValidationProblemDetails>();

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            responseObject.Errors.Should().NotBeEmpty();
        }
    }
}
