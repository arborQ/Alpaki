using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.DreamersControllerTests;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamsGraphQL
{
    public class AdminQueryTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public AdminQueryTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Admin_CanQuery_All()
        {
            // Arrange 
            var dreamCount = 100;
            var ageFrom = 10;
            var ageTo = 50;
            var categoryA = _fixture.DreamCategoryBuilder().Create();
            var categoryB = _fixture.DreamCategoryBuilder().Create();
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(categoryA);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(categoryB);

            var dreamsA = _fixture.DreamBuilder().WithCategory(categoryA).CreateMany(dreamCount / 2).ToList();

            await IntegrationTestsFixture.DatabaseContext.Dreams
                .AddRangeAsync(dreamsA);

            var dreamsB = _fixture.DreamBuilder().WithCategory(categoryB).CreateMany(dreamCount / 2).ToList();

            await IntegrationTestsFixture.DatabaseContext.Dreams
                .AddRangeAsync(dreamsB);

            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserAdminContext();

            // Act

            var allDreamsTask = Client.GetDreams();

            var ageFilterDreamsTask = Client.GetDreams(ageFrom: ageFrom, ageTo: ageTo);

            var ageFromFilterDreamsTask = Client.GetDreams(ageFrom: ageFrom);

            var ageToFilterDreamsTask = Client.GetDreams(ageTo: ageTo);

            var genderMaleFilterDreamsTask = Client.GetDreams(gender:GenderEnum.Male);

            var genderFemaleFilterDreamsTask = Client.GetDreams(gender: GenderEnum.Female);

            var doneStatusFilterDreamsTask = Client.GetDreams(status: DreamStateEnum.Done);

            var categoriesFilterDreamsTask = Client.GetDreams(categories: new[] { categoryA.DreamCategoryId });

            var allCategoriesFilterDreamsTask = Client.GetDreams(categories: new[] { categoryA.DreamCategoryId, categoryB.DreamCategoryId });
 
            await Task.WhenAll(
                allDreamsTask,
                ageFilterDreamsTask,
                ageFromFilterDreamsTask,
                ageToFilterDreamsTask,
                genderMaleFilterDreamsTask,
                genderFemaleFilterDreamsTask,
                doneStatusFilterDreamsTask,
                categoriesFilterDreamsTask,
                allCategoriesFilterDreamsTask
            );

            // Assert
            var dreams = dreamsA.Concat(dreamsB).ToList();
            Assert.Equal(dreamCount, (await allDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.Age >= ageFrom && d.Age <= ageTo).Count(), (await ageFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.Age >= ageFrom).Count(), (await ageFromFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.Age <= ageTo).Count(), (await ageToFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.Gender == GenderEnum.Male).Count(), (await genderMaleFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.Gender == GenderEnum.Female).Count(), (await genderFemaleFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreams.Where(d => d.DreamState == DreamStateEnum.Done).Count(), (await doneStatusFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreamCount / 2, (await categoriesFilterDreamsTask).Dreams.Count());
            Assert.Equal(dreamCount, (await allCategoriesFilterDreamsTask).Dreams.Count());
        }
    }
}
