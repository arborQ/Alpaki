using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.UserControllerTests
{
    public class UserControllerUpdateTests : IntegrationTestsClass
    {
        private readonly Fixture _fixture;

        public UserControllerUpdateTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task UserControllerUpdate_Works()
        {
            // Arrange
            var user = new User { FirstName = "FirstName", LastName = "LastName", Brand = "Brand", Email = "Email", PhoneNumber = "PhoneNumber", Role = UserRoleEnum.Volunteer };

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(user);

            var data = new { userId = user.UserId, firstName = "new name", emailAddress = "lol@o2.pl" }.AsJsonContent();

            // Act
            var response = await Client.PatchAsync("/api/user", data);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("F1", "L1", "email1@ee.com", "B1", "P1")]
        [InlineData("F2", "L2", "email2@ee.com", "B2", "P1")]
        [InlineData("Longer First Name", "Longer last name", "testtest@test.com", "new brand name", "123456643")]
        public async Task UserControllerUpdate_UpdateUser_AndCanQuery(string firstName, string lastName, string email, string brand, string phoneNumber)
        {
            var user = _fixture.VolunteerBuilder().Create();

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(user);

            var data = new { userId = user.UserId, firstName, lastName, email, brand, phoneNumber }.AsJsonContent();

            // Act
            var response  = await Client.PatchAsync("/api/user", data);
            response.EnsureSuccessStatusCode();
            var myData = await Client.GetUsers();

            Assert.Single(myData.Users);
            Assert.Equal(user.UserId, myData.Users.First().UserId);
            Assert.Equal(firstName, myData.Users.First().FirstName);
            Assert.Equal(lastName, myData.Users.First().LastName);
            Assert.Equal(email, myData.Users.First().Email);
            Assert.Equal(brand, myData.Users.First().Brand);
            Assert.Equal(phoneNumber, myData.Users.First().PhoneNumber);
        }

        [Theory]
        [InlineData(null, "L1", "email1@ee.com", "B1", "P1")]
        [InlineData(null, "L1", "email1@ee.com", "B1", null)]
        [InlineData(null, "L1", null, "B1", "P1")]
        [InlineData(null, null, "email1@ee.com", "B1", "P1")]
        [InlineData(null, "L1", null, null, "P1")]
        [InlineData(null, "L1", "email1@ee.com", null, null)]
        [InlineData("F2", "L2", null, "B2", "P1")]
        [InlineData("Longer First Name", "Longer last name", "testtest@test.com", "new brand name", "123456643")]
        public async Task UserControllerUpdate_UpdateUser_IfNotNULLValue(string firstName, string lastName, string email, string brand, string phoneNumber)
        {
            var user = new User { FirstName = "FirstName", LastName = "LastName", Brand = "Brand", Email = "Email", PhoneNumber = "PhoneNumber", Role = UserRoleEnum.Volunteer };

            await IntegrationTestsFixture.DatabaseContext.Users.AddAsync(user);
            await IntegrationTestsFixture.DatabaseContext.SaveChangesAsync();

            IntegrationTestsFixture.SetUserContext(user);

            var data = new { userId = user.UserId, firstName, lastName, email, brand, phoneNumber }.AsJsonContent();

            // Act
            var response = await Client.PatchAsync("/api/user", data);
            response.EnsureSuccessStatusCode();
            var myData = await Client.GetUsers();

            Assert.Single(myData.Users);
            Assert.Equal(user.UserId, myData.Users.First().UserId);
            Assert.Equal(firstName ?? user.FirstName, myData.Users.First().FirstName);
            Assert.Equal(lastName ?? user.LastName, myData.Users.First().LastName);
            Assert.Equal(email ?? user.Email, myData.Users.First().Email);
            Assert.Equal(brand ?? user.Brand, myData.Users.First().Brand);
            Assert.Equal(phoneNumber ?? user.PhoneNumber, myData.Users.First().PhoneNumber);
        }
    }
}
