using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
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
    public class DreamersControllerTests
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly GraphQLClient _graphQL;

        public DreamersControllerTests()
        {
            var factory = new CustomWebApplicationFactory();
            _client = factory.CreateClient();
            _graphQL = new GraphQLClient(_client);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task DreamersController_POST_CreateDreamer()
        {
            // Arrange
            var count = 20;
            var random = new Random();
            var requests = _fixture
                .Build<CreateDreamerRequestFake>()
                .With(d => d.Age, random.Next(1, 119))
                .With(d => d.Gender, GenderEnum.Female)
                .CreateMany(count)
                .Select(dreamer =>
                {
                    var json = JsonConvert.SerializeObject(dreamer);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    return data;
                });
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
            var responses = await Task.WhenAll(requests.Select(r => _client.PostAsync($"/api/dreamers", r)));
            var graphResponse = await _graphQL.Query<DreamerResponse>(query);

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
