using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Tests.IntegrationTests.Extensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.VolunteerDreamAssignTests
{
    public class VolunteerDreamAssignTests : IntegrationTestsClass
    {
        public VolunteerDreamAssignTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
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
            var json = JsonConvert.SerializeObject(new { DreamerId = 1, VolunteerId = 1 });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/VolunteerDreamAssign", data);
            var responseObject = await response.GetResponse<ErrorResponseModel>();

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(2, responseObject.Errors.Keys.Count);
        }
    }
}
