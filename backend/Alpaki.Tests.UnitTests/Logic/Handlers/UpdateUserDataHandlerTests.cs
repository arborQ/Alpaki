using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.UpdateUserData;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Handlers
{
    public class UpdateUserDataHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly UpdateUserDataHandler _updateUserDataHandler;
        private readonly IDatabaseContext _databaseContext;

        public UpdateUserDataHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();

            _updateUserDataHandler = new UpdateUserDataHandler(_databaseContext);
        }

        [Fact]
        public async Task UpdateUserDataHandler_UpdateUser_AndSaveData()
        {
            // Arrange
            var userId = _fixture.Create<long>();
            var request = _fixture.Create<UpdateUserDataRequest>();
            request.UserId = userId;
            var usersList = new List<User> { new User { UserId = userId } }.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            // Act
            await _updateUserDataHandler.Handle(request, default);

            // Assert
            await _databaseContext.Received(1).SaveChangesAsync();
            var editedUser = usersList.First();
            Assert.Equal(request.FirstName, editedUser.FirstName);
            Assert.Equal(request.LastName, editedUser.LastName);
            Assert.Equal(request.Email, editedUser.Email);
            Assert.Equal(request.Brand, editedUser.Brand);
            Assert.Equal(request.PhoneNumber, editedUser.PhoneNumber);
        }

        [Fact]
        public async Task UpdateUserDataHandler_UpdateUser_DontUpdateNullProperties()
        {
            // Arrange
            var userId = _fixture.Create<long>();
            var request = _fixture.Build<UpdateUserDataRequest>()
                .With(r => r.LastName, null as string)
                .With(r => r.Email, null as string)
                .Create();
            var usersList = new List<User> { new User {
                UserId = request.UserId,
                LastName = "original last name",
                Role = UserRoleEnum.Volunteer,
                Email = "original email"
            }
            }.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            // Act
            await _updateUserDataHandler.Handle(request, default);

            // Assert
            await _databaseContext.Received(1).SaveChangesAsync();
            var editedUser = usersList.First();
            Assert.Equal(request.FirstName, editedUser.FirstName);
            Assert.Equal("original email", editedUser.Email);
            Assert.Equal("original last name", editedUser.LastName);
            Assert.Equal(request.Brand, editedUser.Brand);
            Assert.Equal(request.PhoneNumber, editedUser.PhoneNumber);
            Assert.Equal(UserRoleEnum.Volunteer, editedUser.Role);
        }

        [Fact]
        public async Task UpdateUserDataHandler_UpdateUser_Fail_IfUserDoesNotExists()
        {
            // Arrange
            var request = _fixture.Create<UpdateUserDataRequest>();

            var usersList = new List<User> { new User {
                UserId = _fixture.Create<long>()
            }
            }.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            // Act && Assert
            await Assert.ThrowsAnyAsync<Exception>(() => _updateUserDataHandler.Handle(request, default));
        }        
    }
}
