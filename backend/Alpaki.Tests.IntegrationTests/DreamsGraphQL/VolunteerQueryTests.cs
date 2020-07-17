using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.DreamersControllerTests;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamsGraphQL
{
    public class VolunteerQueryTests : IntegrationTestsClass
    {
        private readonly GraphQLClient _graphQLClient;
        private readonly Fixture _fixture;

        public VolunteerQueryTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _graphQLClient = new GraphQLClient(Client);
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(100, 10)]
        [InlineData(100, 50)]
        [InlineData(5, 0)]
        public async Task Volunteer_CanSearch_HisDreams(int allDreamsCount, int volunteerDreamCount)
        {
            // Arrange
            var category = _fixture.DreamCategoryBuilder().Create();
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);

            var volunteerUser = _fixture.VolunteerBuilder().Create();
            var otherVolunteerUser = _fixture.VolunteerBuilder().Create();

            var dreams = _fixture.DreamBuilder().WithCategory(category).CreateMany(allDreamsCount).ToList();
            
            await IntegrationTestsFixture.DatabaseContext.Dreams
                .AddRangeAsync(dreams);
            await IntegrationTestsFixture.DatabaseContext.Users
                .AddAsync(volunteerUser);
            await IntegrationTestsFixture.DatabaseContext.Users
                 .AddAsync(otherVolunteerUser);

            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(dreams.Take(volunteerDreamCount).Select(d => new AssignedDreams { Dream = d, Volunteer = volunteerUser }));
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(dreams.Skip(volunteerDreamCount).Select(d => new AssignedDreams { Dream = d, Volunteer = otherVolunteerUser }));

            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(volunteerUser);

            // Act
            var response = await _graphQLClient.Query<DreamerResponse>(@"
                    query DreamerQuery {
                      dreams {
                        dreamId
                      }
                    }                    
                ");

            // Assert
            Assert.Equal(volunteerDreamCount, response.Dreams.Count());
        }
    }
}
