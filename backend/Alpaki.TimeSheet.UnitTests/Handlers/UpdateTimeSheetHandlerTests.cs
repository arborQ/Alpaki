using System.Threading.Tasks;
using Alpaki.Logic.Handlers.DeleteBrand;
using Alpaki.TimeSheet.Database;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace Alpaki.TimeSheet.UnitTests.Handlers
{
    public class UpdateTimeSheetHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly UpdateTimeSheetHandler _sut;
        private readonly ITimeSheetDatabaseContext _databaseContext;

        public UpdateTimeSheetHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<ITimeSheetDatabaseContext>();

            _sut = new UpdateTimeSheetHandler(_databaseContext);
        }

        [Fact]
        public async Task UpdateTimeSheetHandler_Hendle_Success()
        {
            // Arrange
            var request = new UpdateTimeSheetRequest();

            // Act
            await _sut.Handle(request, default);

            //  Assert
        }
    }
}
