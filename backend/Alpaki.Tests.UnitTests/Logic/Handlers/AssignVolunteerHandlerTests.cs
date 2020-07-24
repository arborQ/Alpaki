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

namespace Alpaki.Tests.UnitTests.Logic.Handlers
{
    public class AssignVolunteerHandlerTests
    {
        private readonly Fixture _fixture;

        public AssignVolunteerHandlerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AssignVolunteerHandler_ShouldAddAssignToDatabase()
        {
            // Arrange
            var databaseContextMock = Substitute.For<IDatabaseContext>();
            var assignDbMock = new List<AssignedDreams>().AsQueryable().BuildMockDbSet();
            databaseContextMock.AssignedDreams.Returns(assignDbMock);
            var sut = new AssignVolunteerHandler(databaseContextMock);
            var request = _fixture.Create<AssignVolunteerRequest>();

            // Act
            await sut.Handle(request, default);

            // Assert
            await databaseContextMock.Received(1).SaveChangesAsync(default);
            await assignDbMock.Received(1).AddAsync(Arg.Any<AssignedDreams>());
            await assignDbMock.Received(1).AddAsync(Arg.Is<AssignedDreams>(ad => ad.DreamId == request.DreamId && ad.VolunteerId == request.VolunteerId));
        }
    }
}
