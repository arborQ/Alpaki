using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Services;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.UserControllerTests
{
    public class UserControllerUpdateTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public UserControllerUpdateTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task UserControllerUpdate_Works()
        {
            // Arrange
            var user = new User { FirstName = "FirstName", LastName = "LastName", Brand = "Brand", Email = "Email", PhoneNumber = "PhoneNumber", Role = UserRoleEnum.Volunteer };

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(user);

            var json = JsonConvert.SerializeObject(new { firstName = "new name", emailAddress = "lol@o2.pl" });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PatchAsync("/api/User/me", data);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
