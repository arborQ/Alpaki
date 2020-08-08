using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.GetCategories;
using Alpaki.Logic.Handlers.UpdateCategory;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.CategoriesControllerTests
{
    public class GetCategoryActionTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;
        private const string ActionUrl = "/api/categories";

        public GetCategoryActionTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
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
                .With(c => c.DefaultSteps, () => _fixture.CreateMany<UpdateCategoryRequest.UpdateCategoryDefaultStep>(2).ToArray()).Create();

            // Act

            var response = await Client.PatchAsync(ActionUrl, request.WithJsonContent().json);

            // Assert
            response.IsSuccessStatusCode.Should().Be(isPermited);
        }

        [Fact]
        public async Task CategoriesController_GetCategories()
        {
            // Act
            var categories = _fixture.DreamCategoryBuilder().CreateMany(10);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddRangeAsync(categories);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Volunteer });
            var response = await Client.GetAsync(ActionUrl).AsResponse<GetCategoriesResponse>();

            // Arrange
            response.Categories.Should().HaveCount(10);
        }
    }
}
