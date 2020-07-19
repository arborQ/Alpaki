﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.Extensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.VolunteerDreamAssignTests
{
    public class VolunteerDreamAssignTests : IntegrationTestsClass
    {
        private const string ApiUrlPath = "/api/Volunteers/assign";
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
            var response = await Client.PostAsync(ApiUrlPath, data);
            var responseObject = await response.GetResponse<ValidationProblemDetails>();

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(2, responseObject.Errors.Keys.Count);
        }

        [Fact(DisplayName = "VolunteerDreamAssign Can't CreateAssign if assign already exists")]
        public async Task VolunteerDreamAssign_CantCreateAssignTwice()
        {
            // Arrange
            var dream = new Dream { FirstName = "FirstName", LastName = "LastName", Age = 10, DreamCategory = new DreamCategory { CategoryName = "test" }, DreamUrl = "", Gender = CrossCutting.Enums.GenderEnum.Female };
            var user = new User { FirstName = "FirstName", LastName = "LastName", Brand = "Brand", Email = "Email", PhoneNumber = "PhoneNumber", Role = UserRoleEnum.Volunteer };

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var data = new { dream.DreamId, VolunteerId = user.UserId }.WithJsonContent().json;

            // Act
            var response1 = await Client.PostAsync(ApiUrlPath, data);
            var response2 = await Client.PostAsync(ApiUrlPath, data);
            var responseObject = await response2.GetResponse<ValidationProblemDetails>();

            // Assert
            response1.EnsureSuccessStatusCode();
            Assert.False(response2.IsSuccessStatusCode);
            Assert.Single(responseObject.Errors);
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

            var data = new { dream.DreamId, VolunteerId = user.UserId }.WithJsonContent();

            // Act
            var response = await Client.PostAsync(ApiUrlPath, data.json);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
