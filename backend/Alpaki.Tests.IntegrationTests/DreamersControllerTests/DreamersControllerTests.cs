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
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;

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
            var count = 20;
            var stepCount = 12;
            var category = _fixture.DreamCategoryBuilder(stepCount).Create();
            var images = _fixture.ImageBuilder().CreateMany(count);
            var volonteers = _fixture.VolunteerBuilder().CreateMany(10);

            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(volonteers);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.Images.AddRangeAsync(images);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            var imageIds = images.Select(i => i.ImageId).ToArray();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });


            var index = 0;
            var random = new Random();
            var requests = _fixture
                .Build<AddDreamRequest>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.CategoryId, category.DreamCategoryId)
                .With(d => d.DreamImageId, () => imageIds.ElementAt(index++))
                .With(d => d.VolunteerIds, () => volonteers.OrderBy(v => new Random().Next()).Take(5).Select(v => v.UserId).ToArray())
                .CreateMany(count)
                .Select(dreamer => dreamer.WithJsonContent().json);

            // Act
            var responses = await Task.WhenAll(requests.Select(r => Client.PostAsync($"/api/dreams", r)));
            var graphResponse = await Client.GetDreams();

            // Assert
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }

            Assert.Equal(count, graphResponse.Dreams.Count);
        }

        [Fact]
        public async Task DreamController_AddDream_AssignVolounteersAndImages()
        {
            var stepCount = 12;
            var category = _fixture.DreamCategoryBuilder(stepCount).Create();
            var image = _fixture.ImageBuilder().Create();
            var volonteers = _fixture.VolunteerBuilder().CreateMany(10);

            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(volonteers);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.Images.AddAsync(image);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });


            var random = new Random();
            var request = _fixture
                .Build<AddDreamRequest>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.CategoryId, category.DreamCategoryId)
                .With(d => d.DreamImageId, image.ImageId)
                .With(d => d.VolunteerIds, () => volonteers.OrderBy(v => random.Next()).Take(5).Select(v => v.UserId).ToArray())
                .Create();

            // Act
            var response = await Client.PostAsync($"/api/dreams", request.AsJsonContent()).AsResponse<AddDreamResponse>();
            var graphResponse = await Client.GetDreams(dreamId: response.DreamId);
            var users = await Client.GetUsers(response.DreamId);

            // Assert
            var dream = graphResponse.Dreams.Single();
            dream.DreamId.Should().Be(response.DreamId);
            dream.DreamImageUrl.Should().Contain(image.ImageId.ToString());
            dream.DreamImageUrl.Should().Be($"/api/images/{image.ImageId}.png");
            users.Users.Count.Should().Be(5);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(2)]
        public async Task DreamController_UpdateDream_AssignVolounteersAndImages(int volounteerCount)
        {
            var stepCount = 12;
            var category = _fixture.DreamCategoryBuilder(stepCount).Create();
            var image = _fixture.ImageBuilder().Create();
            var volonteers = _fixture.VolunteerBuilder().CreateMany(10);
            var dreamBuilder = _fixture
                .DreamBuilder()
                .WithCategory(category);

            dreamBuilder.WithImage();

            var dream = dreamBuilder.Create();

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddRangeAsync(volonteers);
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.Images.AddAsync(image);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });


            var random = new Random();
            var request = _fixture
                .Build<UpdateDreamRequest>()
                .With(d => d.DreamId, dream.DreamId)
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.CategoryId, category.DreamCategoryId)
                .With(d => d.DreamImageId, image.ImageId)
                .With(d => d.VolunteerIds, () => volonteers.OrderBy(v => random.Next()).Take(volounteerCount).Select(v => v.UserId).ToArray())
                .Create();

            // Act
            await Client.PutAsync($"/api/dreams", request.AsJsonContent()).AsResponse<UpdateDreamResponse>();
            var graphResponse = await Client.GetDreams(dreamId: dream.DreamId);
            var users = await Client.GetUsers(dream.DreamId);

            // Assert
            var apiDream = graphResponse.Dreams.Single();
            apiDream.DreamId.Should().Be(dream.DreamId);

            apiDream.DreamImageUrl.Should().Contain(image.ImageId.ToString());
            apiDream.DreamImageUrl.Should().Be($"/api/images/{image.ImageId}.png");

            users.Users.Count.Should().Be(volounteerCount);
        }

        [Fact]
        public async Task DreamController_CantAssingImage_IfInUse()
        {
            // Arrange
            var image = _fixture.ImageBuilder().Create();
            var dream = _fixture
                .DreamBuilder()
                .WithNewCategory()
                .Create();

            var user = _fixture.VolunteerBuilder().With(d => d.ProfileImage, image).Create();

            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.Images.AddAsync(image);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserAdminContext();

            // Act

            var response = await Client.PutAsync("/api/dreams", new UpdateDreamRequest { DreamId = dream.DreamId, DreamImageId = image.ImageId }.AsJsonContent()).AsValidationResponse();

            // Assert
            Assert.Contains(response.Errors.Keys, a => a == nameof(UpdateDreamRequest.DreamImageId));
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

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Coordinator });
            var request = new UpdateDreamRequest
            {
                DreamId = dream.DreamId,
                DisplayName = "Test",
                Age = 3,
                DreamUrl = "https://mam-marzenie.pl/marzenie/2",
                Tags = "tag1, tag2",
                CategoryId = category2.DreamCategoryId
            };

            //Act
            var _ = await Client.PutAsync("/api/dreams", request.WithJsonContent().json).AsResponse<UpdateDreamResponse>();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });
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
                x.DreamCategory.DreamCategoryId.Should().Be(request.CategoryId);
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
