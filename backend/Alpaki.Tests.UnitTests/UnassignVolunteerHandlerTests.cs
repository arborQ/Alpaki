using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AssignVolunteer;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests
{
    public class UnassignVolunteerHandlerTests
    {
        private readonly Fixture _fixture;

        public UnassignVolunteerHandlerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task UnassignVolunteerHandler_ShouldRemoveAssignFromDatabase()
        {
            // Arrange
            var request = _fixture.Create<UnassignVolunteerRequest>();
            var databaseContextMock = Substitute.For<IDatabaseContext>();
            
            var assignDbMock = new List<AssignedDreams> {
                new AssignedDreams { VolunteerId = request.VolunteerId, DreamId = request.DreamId }
            }
            .AsQueryable()
            .BuildMockDbSet();

            databaseContextMock.AssignedDreams.Returns(assignDbMock);
            var sut = new UnassignVolunteerHandler(databaseContextMock);

            // Act
            await sut.Handle(request, default);

            // Assert
            await databaseContextMock.Received(1).SaveChangesAsync(default);
            assignDbMock.Received(1).Remove(Arg.Any<AssignedDreams>());
            assignDbMock.Received(1).Remove(Arg.Is<AssignedDreams>(ad => ad.DreamId == request.DreamId && ad.VolunteerId == request.VolunteerId));
        }

        [Fact]
        public async Task UnassignVolunteerHandler_ShouldThrowException_IfAssignDoesNotExists()
        {
            // Arrange
            var request = _fixture.Create<UnassignVolunteerRequest>();
            var databaseContextMock = Substitute.For<IDatabaseContext>();

            var assignDbMock = _fixture
                .Build<AssignedDreams>()
                .Without(ad => ad.Dream)
                .Without(ad => ad.Volunteer)
                .CreateMany(10)
            .AsQueryable()
            .BuildMockDbSet();

            databaseContextMock.AssignedDreams.Returns(assignDbMock);
            var sut = new UnassignVolunteerHandler(databaseContextMock);

            // Act & Assert
            await Assert.ThrowsAnyAsync<Exception>(() => sut.Handle(request, default));

            // Assert
            await databaseContextMock.DidNotReceive().SaveChangesAsync(default);
            assignDbMock.DidNotReceive().Remove(Arg.Any<AssignedDreams>());
        }
    }
}
