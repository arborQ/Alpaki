using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using GraphQL;
using Newtonsoft.Json;
using Xunit;

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
        }
    }
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
            var category = new DreamCategory { CategoryName = "test" };
            await IntegrationTestsFixture.DatabaseContext.DreamCategories.AddAsync(category);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(new User { Role = UserRoleEnum.Admin });
            var graphQL = new GraphQLClient(Client);

            var count = 20;
            var random = new Random();
            var requests = _fixture
                .Build<CreateDreamerRequestFake>()
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
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }
        }
    }
}
