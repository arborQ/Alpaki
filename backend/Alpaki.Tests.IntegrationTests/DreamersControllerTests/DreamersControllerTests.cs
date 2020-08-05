using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.Logic.Handlers.UpdateDreamer;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using GraphQL;
using Xunit;
using Xunit.Abstractions;

namespace Alpaki.Tests.IntegrationTests.DreamersControllerTests
{
    public class DreamerResponse
    {
        public DreamItem[] Dreams { get; set; }

        public class DreamItem
        {
            public long DreamId { get; set; }

            public long Age { get; set; }

            public GenderEnum Gender { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
            
            public string DreamUrl { get; set; }
            
            public string Tags { get; set; }
            
            public DreamCategoryItem DreamCategory { get; set; }            
            public RequiredStep[] RequiredSteps { get; set; }

            public class RequiredStep
            {
                public long DreamStepId { get; set; }

                public string StepDescription { get; set; }

                public string StepState { get; set; }
            }

            public class DreamCategoryItem
            {
                public long DreamCategoryId { get; set; }
            }
        }
    }
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
            var graphQL = new GraphQLClient(Client);

            var count = 20;
            var random = new Random();
            var requests = _fixture
                .Build<CreateDreamerRequest>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.Gender, GenderEnum.Female)
                .With(d => d.CategoryId, category.DreamCategoryId)
                .CreateMany(count)
                .Select(dreamer => dreamer.WithJsonContent().json);

            var query = @"
                    query DreamerQuery {
                      dreams {
                        dreamId
                        age
                        gender
                        firstName
                        lastName
                        requiredSteps {
                         stepDescription
                         stepState
                        }
                      }
                    }                    
                ";
            var dreamersRequest = new GraphQLRequest
            {
                Query = query
            };

            // Act
            var responses = await Task.WhenAll(requests.Select(r => Client.PostAsync($"/api/dreamers", r)));
            var graphResponse = await graphQL.Query<DreamerResponse>(query);

            // Assert
            Assert.Equal(count, graphResponse.Dreams.Length);
            Assert.All(graphResponse.Dreams, d =>
            {
                d.RequiredSteps.Should().HaveCount(stepCount);
            });
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
                FirstName = "T",
                LastName = "T",
                Age = 2,
                Gender = GenderEnum.Female,
                DreamUrl = "https://mam-marzenie.pl/marzenie/1",
                Tags = "tag1",
                DreamCategoryId = category.DreamCategoryId
            };
            await IntegrationTestsFixture.DatabaseContext.Dreams.AddAsync(dream);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();
            
            IntegrationTestsFixture.SetUserContext(new User{Role = UserRoleEnum.Volunteer});
            var request = new UpdateDreamerRequest
            {
                DreamId = dream.DreamId,
                FirstName = "Test",
                LastName = "Test",
                Age = 3,
                Gender = GenderEnum.Male,
                DreamUrl = "https://mam-marzenie.pl/marzenie/2",
                Tags = "tag1, tag2",
                DreamCategoryId = category2.DreamCategoryId
            };
            
            //Act
            var response = await Client.PutAsync("/api/dreamers", request.WithJsonContent().json);

            //Assert
            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            
            var query = @"
                    query DreamerQuery {
                      dreams {
                        dreamId
                        firstName
                        lastName
                        age
                        gender
                        dreamUrl
                        tags
                        dreamCategory{
                            dreamCategoryId
                        }                      
                      }
                    }                    
                ";
            
            IntegrationTestsFixture.SetUserContext(new User{Role = UserRoleEnum.Admin});
            var graphQL = new GraphQLClient(Client);
            var graphResponse = await graphQL.Query<DreamerResponse>(query);
            
            graphResponse.Should().NotBeNull();
            graphResponse.Dreams.Length.Should().Be(1);
            graphResponse.Dreams.Should().SatisfyRespectively(x =>
            {
                x.DreamId.Should().Be(dream.DreamId);
                x.FirstName.Should().Be(request.FirstName);
                x.LastName.Should().Be(request.LastName);
                x.Age.Should().Be(request.Age);
                x.Gender.Should().Be(request.Gender);
                x.DreamUrl.Should().Be(request.DreamUrl);
                x.Tags.Should().Be(request.Tags);
                x.DreamCategory.DreamCategoryId.Should().Be(request.DreamCategoryId);
            });
        }
    }
}
