using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Tests.IntegrationTests.DreamersControllerTests;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamsGraphQL
{
    public class AdminQueryTests : IntegrationTestsClass
    {
        private readonly GraphQLClient _graphQLClient;
        private readonly Fixture _fixture;

        public AdminQueryTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _graphQLClient = new GraphQLClient(Client);
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
            var allDreamsTask = _graphQLClient.Query<DreamerResponse>(@"
                    query DreamerQuery {
                      dreams {
                        dreamId
                      }
                    } 
                ");

            var ageFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@$"
                    query DreamerQuery {{
                      dreams(ageFrom:{ageFrom}  ageTo:{ageTo}) {{
                        dreamId
                      }}
                    }}
                ");

            var ageFromFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@$"
                    query DreamerQuery {{
                      dreams(ageFrom:{ageFrom}) {{
                        dreamId
                      }}
                    }}
                ");

            var ageToFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@$"
                    query DreamerQuery {{
                      dreams(ageTo:{ageTo}) {{
                        dreamId
                      }}
                    }}
                ");

            var genderMaleFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@"
                    query DreamerQuery {
                      dreams(gender:male) {
                        dreamId
                      }
                    }
                ");

            var genderFemaleFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@"
                    query DreamerQuery {
                      dreams(gender:female) {
                        dreamId
                      }
                    }
                ");

            var doneStatusFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@"
                    query DreamerQuery {
                      dreams(status:done) {
                        dreamId
                      }
                    }
                ");

            var categoriesFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@$"
                    query DreamerQuery {{
                      dreams(categories:[{ categoryA.DreamCategoryId }]) {{
                        dreamId
                      }}
                    }}
                ");

            var allCategoriesFilterDreamsTask = _graphQLClient.Query<DreamerResponse>(@$"
                    query DreamerQuery {{
                      dreams(categories:[{ categoryA.DreamCategoryId }, { categoryB.DreamCategoryId }]) {{
                        dreamId
                      }}
                    }}
                ");

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
