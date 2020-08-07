using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database.Models;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.DreamersControllerTests;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using Alpaki.Tests.IntegrationTests.UserControllerTests;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamsGraphQL
{
    public class VolunteerQueryTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public VolunteerQueryTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
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
            var response = await Client.GetDreams();

            // Assert
            Assert.Equal(volunteerDreamCount, response.Dreams.Count());
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(1, 10)]
        [InlineData(10, 10)]
        [InlineData(20, 30)]
        public async Task Volunteer_CanSeeVolounteers_AssignedToHisDreams(int dreamUsersCount, int otherUsersCount)
        {
            // Arrange
            var volunteerUsers = _fixture.VolunteerBuilder().CreateMany(dreamUsersCount);
            var otherDreamVolunteers = _fixture.VolunteerBuilder().CreateMany(otherUsersCount);
            var dreams = _fixture.DreamBuilder().WithNewCategory().CreateMany(2).ToList();

            await IntegrationTestsFixture.DatabaseContext.Dreams
                .AddRangeAsync(dreams);
            await IntegrationTestsFixture.DatabaseContext.Users
                .AddRangeAsync(volunteerUsers);
            await IntegrationTestsFixture.DatabaseContext.Users
                 .AddRangeAsync(otherDreamVolunteers);

            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(volunteerUsers.Select(u => new AssignedDreams { Volunteer = u, Dream = dreams.First() }));
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(otherDreamVolunteers.Select(u => new AssignedDreams { Volunteer = u, Dream = dreams.Last() }));

            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            IntegrationTestsFixture.SetUserContext(volunteerUsers.First());

            // Act
            var response = await Client.GetUsers();

            //Assert
            response.Users.Should().HaveCount(dreamUsersCount);
        }


        [Theory]
        [InlineData(10, 5)]
        [InlineData(1, 10)]
        [InlineData(10, 10)]
        [InlineData(20, 30)]
        public async Task Volunteer_CanFilterVolounteers_AssignedToHisDreams(int dreamUsersCount, int otherUsersCount)
        {
            // Arrange
            var volunteerUsers = _fixture.VolunteerBuilder().CreateMany(dreamUsersCount);
            var otherDreamVolunteers = _fixture.VolunteerBuilder().CreateMany(otherUsersCount);
            var dreams = _fixture.DreamBuilder().WithNewCategory().CreateMany(2).ToList();

            await IntegrationTestsFixture.DatabaseContext.Dreams
                .AddRangeAsync(dreams);
            await IntegrationTestsFixture.DatabaseContext.Users
                .AddRangeAsync(volunteerUsers);
            await IntegrationTestsFixture.DatabaseContext.Users
                 .AddRangeAsync(otherDreamVolunteers);

            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(volunteerUsers.Select(u => new AssignedDreams { Volunteer = u, Dream = dreams.First() }));
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddRangeAsync(otherDreamVolunteers.Select(u => new AssignedDreams { Volunteer = u, Dream = dreams.Last() }));
            await IntegrationTestsFixture.DatabaseContext.AssignedDreams.AddAsync(new AssignedDreams { Volunteer = volunteerUsers.First(), Dream = dreams.Last() });

            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            IntegrationTestsFixture.SetUserContext(volunteerUsers.First());

            // Act
            var response1 = await Client.GetUsers(dreams.First().DreamId);
            var response2 = await Client.GetUsers(dreams.Last().DreamId);
            var response3 = await Client.GetUsers();

            //Assert
            response1.Users.Should().HaveCount(dreamUsersCount);
            response2.Users.Should().HaveCount(otherUsersCount + 1);
            response3.Users.Should().HaveCount(dreamUsersCount + otherUsersCount);
        }
    }
}
