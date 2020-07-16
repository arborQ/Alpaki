using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.IntegrationTests.Fixtures;
using Alpaki.Tests.IntegrationTests.UserControllerTests;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.DreamsGraphQL
{
    public class CoordinatorQueryTests : IntegrationTestsClass
    {
        private readonly GraphQLClient _graphQLClient;

        public CoordinatorQueryTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _graphQLClient = new GraphQLClient(Client);
        }

        [Fact]
        public async Task Coordinator_CanSearch_AllVolunteers()
        {
            // Arrange
            IntegrationTestsFixture.SetUserCoordinatorContext();

            IntegrationTestsFixture.DatabaseContext.Users
                .AddRange(Enumerable.Range(0, 30)
                .Select(i => new User()
                {
                    FirstName = $"property_{i}",
                    LastName = $"property_{i}",
                    Email = $"property_{i}",
                    Brand = $"property_{i}",
                    PhoneNumber = $"property_{i}",
                    Role = i < 10 ? UserRoleEnum.Volunteer : i < 20 ? UserRoleEnum.Coordinator : UserRoleEnum.Admin
                }));
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            // Act
            var response = await _graphQLClient.Query<UserResponse>(@"
                    query DreamerQuery {
                      users {
                        userId,
                        firstName,
                        lastName,
                        email,
                        brand,
                        phoneNumber
                      }
                    } 
            ");

            // Assert
            Assert.NotEmpty(response.Users);
            Assert.Equal(10, response.Users.Length);
        }
    }
}
