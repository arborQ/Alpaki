using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.CategoriesControllerTests
{
    public class AddCategoryActionTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;
        private const string ActionUrl = "/api/Categories";

        public AddCategoryActionTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(UserRoleEnum.Admin, true)]
        [InlineData(UserRoleEnum.Coordinator, true)]
        [InlineData(UserRoleEnum.Volunteer, false)]
        [InlineData(UserRoleEnum.None, false)]
        public async Task CategoriesController_AddCategoryAction_CoordinatorAndAdminCanTrigger(UserRoleEnum role, bool isPermited)
        {
            // Arrange
            IntegrationTestsFixture.SetUserContext(new User { Role = role });
            // Act

            var response = await Client.PostAsync(ActionUrl, new { categoryName = "test" }.WithJsonContent().json);

            // Assert
            response.IsSuccessStatusCode.Should().Be(isPermited);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("Jadę 🙂🐈")]
        public async Task CategoriesController_AddCategoryAction_OnlyOneCategoryWithNameAllowed(string categoryName)
        {
            // Arrange
            IntegrationTestsFixture.SetUserCoordinatorContext();
            var request = new { categoryName }.WithJsonContent().json;
            // Act

            var response1 = await Client.PostAsync(ActionUrl, request);
            var response2 = await Client.PostAsync(ActionUrl, request);

            // Assert
            response1.EnsureSuccessStatusCode();
            response2.IsSuccessStatusCode.Should().BeFalse();

            var dbCategory = await IntegrationTestsFixture.DatabaseContext.DreamCategories.SingleAsync(c => c.CategoryName == categoryName);
            dbCategory.Should().NotBeNull();
        }
    }

}
