using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AddCategory;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Handlers
{
    public class AddCategoryHandlerTests
    {
        private readonly Fixture _fixture;

        public AddCategoryHandlerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddCategoryHandler_ShouldAddCategoryToDatabase()
        {
            // Arrange
            var databaseContextMock = Substitute.For<IDatabaseContext>();
            var categoriesDbMock = new List<DreamCategory>().AsQueryable().BuildMockDbSet();
            databaseContextMock.DreamCategories.Returns(categoriesDbMock);
            var sut = new AddCategoryHandler(databaseContextMock);
            var request = _fixture.Create<AddCategoryRequest>();

            // Act
            await sut.Handle(request, default);

            // Assert
            await databaseContextMock.Received(1).SaveChangesAsync(default);
            await categoriesDbMock.Received(1).AddAsync(Arg.Any<DreamCategory>());
            await categoriesDbMock.Received(1).AddAsync(Arg.Is<DreamCategory>(ad => ad.CategoryName == request.CategoryName));
            await categoriesDbMock.Received(1).AddAsync(Arg.Is<DreamCategory>(ad => ad.DefaultSteps.Count == request.DefaultSteps.Length));
        }
    }
}
