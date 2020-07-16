using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.UpdateUserData;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests
{
    public class UpdateUserDataHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly UpdateUserDataHandler _updateUserDataHandler;
        private readonly IDatabaseContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserDataHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();
            _currentUserService = Substitute.For<ICurrentUserService>();

            _updateUserDataHandler = new UpdateUserDataHandler(_databaseContext, _currentUserService);
        }

        [Fact]
        public async Task UpdateUserDataHandler_UpdateUser_AndSaveData()
        {
            // Arrange
            var userId = _fixture.Create<long>();
            var request = _fixture.Create<UpdateUserDataRequest>();
            _currentUserService.CurrentUserId.Returns(userId);
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
            _currentUserService.CurrentUserId.Returns(userId);
            var usersList = new List<User> { new User {
                UserId = userId,
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
            var userId = _fixture.Create<long>();
            var request = _fixture.Create<UpdateUserDataRequest>();

            _currentUserService.CurrentUserId.Returns(userId);
            var usersList = new List<User> { new User {
                UserId = _fixture.Create<long>()
            }
            }.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);

            // Act && Assert
            await Assert.ThrowsAnyAsync<Exception>(() => _updateUserDataHandler.Handle(request, default));

        }

        [Fact]
        public async Task UpdateUserDataRequestValidator_DoesNotReturnError_IfPropertyIsNull()
        {
            // Arrange
            var validator = new UpdateUserDataRequestValidator();

            // Act
            var result = await validator.ValidateAsync(new UpdateUserDataRequest(), default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task UpdateUserDataRequestValidator_DoesReturnError_IfPropertyIsInvalid()
        {
            // Arrange
            var validator = new UpdateUserDataRequestValidator();
            var request = new UpdateUserDataRequest
            {
                FirstName = "", LastName = "", Brand = "", Email = "not email", PhoneNumber = ""
            };

            // Act
            var result = await validator.ValidateAsync(request, default);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.FirstName));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.LastName));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.Brand));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.Email));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.PhoneNumber));
        }
    }
}
