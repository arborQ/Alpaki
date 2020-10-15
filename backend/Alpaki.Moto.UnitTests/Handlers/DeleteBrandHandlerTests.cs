using System.Threading.Tasks;
using Alpaki.Logic.Handlers.DeleteBrand;
using Alpaki.Moto.Database;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace Alpaki.Moto.UnitTests.Handlers
{
    public class DeleteBrandHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly DeleteBrandHandler _sut;
        private readonly IMotoDatabaseContext _databaseContext;

        public DeleteBrandHandlerTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IMotoDatabaseContext>();

            _sut = new DeleteBrandHandler(_databaseContext);
        }

        [Fact]
        public async Task DeleteBrandHandler_Hendle_Success()
        {
            // Arrange
            var request = new DeleteBrandRequest();

            // Act
            await _sut.Handle(request, default);

            //  Assert
        }
    }
}
