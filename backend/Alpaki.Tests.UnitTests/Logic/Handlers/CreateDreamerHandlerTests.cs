using System.Linq;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AddDream;
using Alpaki.Tests.Common.Builders;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Handlers
{
    public class CreateDreamerHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly IDatabaseContext _databaseContext;
        private readonly AddDreamHandler _sut;

        public CreateDreamerHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();
            _sut = new AddDreamHandler(_databaseContext);
        }

        [Fact]
        public async Task CreateDreamerHandler_Handle_AddDream()
        {
            // Arrange
            var request = _fixture.Create<AddDreamRequest>();
            var dreamsDbMock = _fixture.DreamBuilder().CreateMany(10).AsQueryable().BuildMockDbSet();
            var steps = _fixture.DreamCategoryDefaultStepBuilder().With(s => s.DreamCategoryId, request.CategoryId).CreateMany(20).AsQueryable().BuildMockDbSet();
            _databaseContext.Dreams.Returns(dreamsDbMock);
            _databaseContext.DreamCategoryDefaultSteps.Returns(steps);

            // Act
            await _sut.Handle(request, default);

            // Assert
            await _databaseContext.Received(1).SaveChangesAsync(default);
            await dreamsDbMock.Received(1).AddAsync(Arg.Any<Dream>());
            await dreamsDbMock.Received(1).AddAsync(Arg.Is<Dream>(ad => ad.DisplayName == request.DisplayName));
            await dreamsDbMock.Received(1).AddAsync(Arg.Is<Dream>(ad => ad.Age == request.Age));
            await dreamsDbMock.Received(1).AddAsync(Arg.Is<Dream>(ad => ad.DreamCategoryId == request.CategoryId));
            await dreamsDbMock.Received(1).AddAsync(Arg.Is<Dream>(ad => ad.DreamUrl == request.DreamUrl));
            await dreamsDbMock.Received(1).AddAsync(Arg.Is<Dream>(ad => ad.RequiredSteps.Count == steps.Count()));
        }

        [Theory]
        [InlineData(10, 2)]
        [InlineData(1, 2)]
        [InlineData(0, 2)]
        [InlineData(10, 0)]
        public async Task CreateDreamerHandler_Handle_AddDream_WithRequiredSponsor(int sponsorStepCount, int normalStepCount)
        {
            // Arrange
            var request = _fixture.Build<AddDreamRequest>().With(r => r.IsSponsorRequired, false).Create();
            var dreamsDbMock = _fixture.DreamBuilder().CreateMany(10).AsQueryable().BuildMockDbSet();
            var stepBuilder = _fixture.DreamCategoryDefaultStepBuilder().With(s => s.DreamCategoryId, request.CategoryId);
            var normalSteps = stepBuilder.With(s => s.IsSponsorRelated, false).CreateMany(normalStepCount);
            var sponsorSteps  = stepBuilder.With(s => s.IsSponsorRelated, true).CreateMany(sponsorStepCount);

            var steps = normalSteps.Concat(sponsorSteps).AsQueryable().BuildMockDbSet();
            _databaseContext.Dreams.Returns(dreamsDbMock);
            _databaseContext.DreamCategoryDefaultSteps.Returns(steps);

            // Act
            await _sut.Handle(request, default);

            // Assert
            await dreamsDbMock.Received().AddAsync(Arg.Is<Dream>(ad => ad.RequiredSteps.Count(s => s.StepState == CrossCutting.Enums.StepStateEnum.Skiped) == sponsorStepCount));
            await dreamsDbMock.Received().AddAsync(Arg.Is<Dream>(ad => ad.RequiredSteps.Count(s => s.StepState == CrossCutting.Enums.StepStateEnum.Awaiting) == normalStepCount));
            await dreamsDbMock.Received().AddAsync(Arg.Is<Dream>(ad => ad.RequiredSteps.Count() == normalStepCount + sponsorStepCount));
        }
    }
}
