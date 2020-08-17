using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.GetDreams;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using FluentAssertions;
using GraphQL;
using Xunit;
using Xunit.Abstractions;
using Alpaki.Logic.Handlers.AddDream;
using Alpaki.Logic.Handlers.UpdateDream;

namespace Alpaki.Tests.IntegrationTests.DreamersControllerTests
{
    public class DreamersControllerTests : IntegrationTestsClass
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Fixture _fixture;

        public DreamersControllerTests(IntegrationTestsFixture integrationTestsFixture, ITestOutputHelper testOutputHelper) : base(integrationTestsFixture)
        {
            _testOutputHelper = testOutputHelper;
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
                .Build<AddDreamRequest>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.CategoryId, category.DreamCategoryId)
                .CreateMany(count)
                .Select(dreamer => dreamer.WithJsonContent().json);

            // Act
            var responses = await Task.WhenAll(requests.Select(r => Client.PostAsync($"/api/dreams", r)));
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
        public async Task DreamersController_PUT_UpdateDreamer()
        {
            //Arrange
            var category = _fixture.DreamCategoryBuilder(10).Create();
            var category2 = _fixture.DreamCategoryBuilder(15).Create();
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category2);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            var dream = new Dream
            {
                DisplayName = "T",
                Age = 2,
                DreamUrl = "https://mam-marzenie.pl/marzenie/1",
                Tags = "tag1",
                DreamCategoryId = category.DreamCategoryId
            };
            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            
            IntegrationTestsFixture.SetUserContext(new User{Role = UserRoleEnum.Coordinator});
            var request = new UpdateDreamRequest
            {
                DreamId = dream.DreamId,
                DisplayName = "Test",
                Age = 3,
                DreamUrl = "https://mam-marzenie.pl/marzenie/2",
                Tags = "tag1, tag2",
                DreamCategoryId = category2.DreamCategoryId
            };
            
            //Act
            var _ = await Client.PutAsync("/api/dreams", request.WithJsonContent().json).AsResponse<UpdateDreamResponse>();
            
            IntegrationTestsFixture.SetUserContext(new User{Role = UserRoleEnum.Admin});
            var queryResponse = await Client.GetDreams();
            
            
            queryResponse.Should().NotBeNull();
            queryResponse.Dreams.Count.Should().Be(1);
            queryResponse.Dreams.Should().SatisfyRespectively(x =>
            {
                x.DreamId.Should().Be(dream.DreamId);
                x.DisplayName.Should().Be(request.DisplayName);
                x.Age.Should().Be(request.Age);
                x.DreamUrl.Should().Be(request.DreamUrl);
                x.Tags.Should().Be(request.Tags);
                x.DreamCategory.Should().NotBeNull();
                x.DreamCategory.DreamCategoryId.Should().Be(request.DreamCategoryId);
            });
        }
        public async Task DreamerScontroller_GET_ByDreamId()
        {
            // Arrange
            var dreams = _fixture.DreamBuilder().WithNewCategory().CreateMany(10).ToList();
            await IntegrationTestsFixture.DatabaseContext.Dreams.AddRangeAsync(dreams);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            IntegrationTestsFixture.SetUserAdminContext();
            // Act
            var responses = await Task.WhenAll(dreams.Select(d => Client.GetAsync($"/api/dreams/details?dreamId={d.DreamId}").AsResponse<GetDreamResponse>()));

            // Assert
            foreach (var response in responses)
            {
                response.DisplayName.Should().NotBeNullOrEmpty();
            }
        }
    }
}
