using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.Extensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.VolunteerDreamAssignTests
{
    public class VolunteerDreamAssignTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public VolunteerDreamAssignTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName = "VolunteerDreamAssign Can't CreateAssign if there is no user or dream")]
        public async Task VolunteerDreamAssign_CantCreateAssign()
        {
            // Arrange
            var json = JsonConvert.SerializeObject(new { DreamerId = 1, VolunteerId = 1 });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/VolunteerDreamAssign", data);
            var responseObject = await response.GetResponse<ErrorResponseModel>();

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(2, responseObject.Errors.Keys.Count);
        }

        [Fact(DisplayName = "VolunteerDreamAssign Can CreateAssign if there is user or dream")]
        public async Task VolunteerDreamAssign_CanCreateAssign()
        {
            // Arrange
            var dream = new Dream { FirstName = "FirstName", LastName = "LastName", Age = 10, DreamCategory = new DreamCategory { CategoryName = "test" }, DreamUrl = "", Gender = CrossCutting.Enums.GenderEnum.Female };
            var user = new User { FirstName = "FirstName", LastName = "LastName", Brand = "Brand", Email = "Email", PhoneNumber = "PhoneNumber", Role = UserRoleEnum.Volunteer  };

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var json = JsonConvert.SerializeObject(new { dream.DreamId, VolunteerId = user.UserId });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/VolunteerDreamAssign", data);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
