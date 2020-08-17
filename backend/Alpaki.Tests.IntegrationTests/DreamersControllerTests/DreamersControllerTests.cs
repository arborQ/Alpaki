using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.Logic.Handlers.GetDreams;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using GraphQL;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamersControllerTests
{
    public class DreamersControllerTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public DreamersControllerTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task DreamersController_POST_CreateDreamer()
        {
            // Arrange
            var stepCount = 12;
            var category = _fixture.DreamCategoryBuilder(stepCount).Create();
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });

            var count = 20;
            var random = new Random();
            var requests = _fixture
                .Build<CreateDreamerRequest>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.CategoryId, category.DreamCategoryId)
                .CreateMany(count)
                .Select(dreamer => dreamer.WithJsonContent().json);

            // Act
            var responses = await Task.WhenAll(requests.Select(r => Client.PostAsync($"/api/dreamers", r)));
            var graphResponse = await Client.GetDreams();

            // Assert
            Assert.Equal(count, graphResponse.Dreams.Count);

            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }
        }

        [Fact]
        public async Task DreamerScontroller_GET_ByDreamId()
        {
            // Arrange
            var dreams = _fixture.DreamBuilder().WithNewCategory().CreateMany(10).ToList();
            await IntegrationTestsFixture.DatabaseContext.Dreams.AddRangeAsync(dreams);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            IntegrationTestsFixture.SetUserAdminContext();
            // Act
            var responses = await Task.WhenAll(dreams.Select(d => Client.GetAsync($"/api/dreamers/details?dreamId={d.DreamId}").AsResponse<GetDreamResponse>()));

            // Assert
            foreach (var response in responses)
            {
                response.DisplayName.Should().NotBeNullOrEmpty();
            }
        }
    }
}
