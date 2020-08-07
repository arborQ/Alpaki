using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AddCategory;
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
            var categoryName = _fixture.Create<string>();

            // Act

            var response = await Client.PostAsync(ActionUrl, new { categoryName, defaultSteps = new[] { new { stepName = "test" } } }.WithJsonContent().json);

            // Assert
            response.IsSuccessStatusCode.Should().Be(isPermited);
        }

        [Theory]
        [InlineData("test", 10)]
        [InlineData("Jadę 🙂🐈", 20)]
        [InlineData("Chcę dostać", 1)]
        public async Task CategoriesController_AddCategoryAction_OnlyOneCategoryWithNameAllowed(string categoryName, int stepCount)
        {
            // Arrange
            IntegrationTestsFixture.SetUserCoordinatorContext();
            var request = _fixture.Build<AddCategoryRequest>()
                .With(c => c.CategoryName, categoryName)
                .With(c => c.DefaultSteps, () => _fixture.CreateMany<AddCategoryRequest.AddCategoryDefaultStep>(stepCount).ToArray()).Create();

            // Act

            var response1 = await Client.PostAsync(ActionUrl, request.WithJsonContent().json);
            var response2 = await Client.PostAsync(ActionUrl, request.WithJsonContent().json);

            // Assert
            response1.EnsureSuccessStatusCode();
            response2.IsSuccessStatusCode.Should().BeFalse();

            var dbCategory = await IntegrationTestsFixture.DatabaseContext.DreamCategories.Include(c => c.DefaultSteps).SingleAsync(c => c.CategoryName == categoryName);
            dbCategory.Should().NotBeNull();
            dbCategory.DefaultSteps.Should().HaveCount(stepCount);

            foreach(var step in request.DefaultSteps)
            {
                var dbStep = dbCategory.DefaultSteps.SingleOrDefault(s => s.StepDescription == step.StepName);
                dbStep.Should().NotBeNull();
            }
        }
    }

}
