using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AddCategory;
using Alpaki.Logic.Handlers.UpdateCategory;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.CategoriesControllerTests
{
    public class UpdateCategoryActionTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;
        private const string ActionUrl = "/api/Categories";

        public UpdateCategoryActionTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
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

            var category = _fixture.DreamCategoryBuilder().Create();
            IntegrationTestsFixture.DatabaseContext.DreamCategories.Add(category);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            var request = _fixture.Build<UpdateCategoryRequest>()
                .With(c => c.CategoryId, category.DreamCategoryId)
                .With(c => c.DefaultSteps, () => _fixture.CreateMany<UpdateCategoryRequest.CategoryDefaultStep>(2).ToArray()).Create();

            // Act

            var response = await Client.PatchAsync(ActionUrl, request.WithJsonContent().json);

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
            var category = _fixture.DreamCategoryBuilder().Create();
            IntegrationTestsFixture.DatabaseContext.DreamCategories.Add(category);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserCoordinatorContext();
            var request = _fixture.Build<UpdateCategoryRequest>()
                .With(c => c.CategoryName, categoryName)
                .With(c => c.CategoryId, category.DreamCategoryId)
                .With(c => c.DefaultSteps, () => _fixture.Build<UpdateCategoryRequest.CategoryDefaultStep>().With(c => c.CategoryDefaultStepId, 0).CreateMany(stepCount).ToArray()).Create();

            // Act

            var response = await Client.PatchAsync(ActionUrl, request.WithJsonContent().json);

            // Assert
            response.EnsureSuccessStatusCode();

            var dbCategory = await IntegrationTestsFixture.DatabaseContext.DreamCategories.Include(c => c.DefaultSteps).SingleAsync(c => c.CategoryName == categoryName);
            await Task.WhenAll(dbCategory.DefaultSteps.ToList().Select(s => IntegrationTestsFixture.DatabaseContext.ReloadAsync(s)));

            dbCategory.Should().NotBeNull();
            dbCategory.DefaultSteps.Should().HaveCount(stepCount);

            foreach (var step in request.DefaultSteps)
            {
                var dbStep = dbCategory.DefaultSteps.SingleOrDefault(s => s.StepDescription == step.StepName);
                dbStep.Should().NotBeNull();
                dbStep.IsSponsorRelated.Should().Be(step.IsSponsorRelated);
            }
        }
    }

}
