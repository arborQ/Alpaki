using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.DeleteUser;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.DeleteUser
{
    public class DeleteUserHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly DeleteUserHandler _sut;
        private readonly IDatabaseContext _databaseContext;

        public DeleteUserHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();

            _sut = new DeleteUserHandler(_databaseContext);
        }

        [Fact]
        public async Task DeleteUserHandler_ThrowException_IfThereIsNoUser()
        {
            // Arrange
            var users = new List<User>().AsQueryable().BuildMockDbSet();
            var assignedDreams = new List<AssignedDreams>().AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(users);
            _databaseContext.AssignedDreams.Returns(assignedDreams);

            // Act && Assert
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await _sut.Handle(_fixture.Create<DeleteUserRequest>(), default);
            });
        }

        [Fact]
        public async Task DeleteUserHandler_SaveData_IfUserExists_AndNoAssinedDreams()
        {
            // Arrange
            var userId = _fixture.Create<long>();
            var users = new List<User> { new User { UserId = userId } }.AsQueryable().BuildMockDbSet();
            var assignedDreams = new List<AssignedDreams>().AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(users);
            _databaseContext.AssignedDreams.Returns(assignedDreams);

            // Act
            await _sut.Handle(new DeleteUserRequest { UserId = userId }, default);

            // Assert
            await _databaseContext.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteUserHandler_SaveData_IfUserExists_AndAssinedDreams()
        {
            // Arrange
            var userId = _fixture.Create<long>();
            var users = new List<User> { new User { UserId = userId } }.AsQueryable().BuildMockDbSet();
            var assignedDreams = new List<AssignedDreams> { new AssignedDreams { VolunteerId = userId, DreamId = 1 } }.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(users);
            _databaseContext.AssignedDreams.Returns(assignedDreams);

            // Act
            await _sut.Handle(new DeleteUserRequest { UserId = userId }, default);

            // Assert
            await _databaseContext.Received(1).SaveChangesAsync();
        }
    }
}
