using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Handlers.AddCategory;
using AutoFixture;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Validators
{
    public class AddCategoryRequestValidatorTests
    {
        private readonly Fixture _fixture;
        private readonly AddCategoryRequestValidator _sut;
        private readonly IDatabaseContext _databaseContext;
        private readonly IReadOnlyCollection<DreamCategory> _existingCategories;

        public AddCategoryRequestValidatorTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IDatabaseContext>();
            _existingCategories = _fixture.Build<DreamCategory>().Without(c => c.Dreams).CreateMany(10).ToList();
            var queryMock = _existingCategories
                .AsQueryable()
                .BuildMockDbSet();
            _databaseContext.DreamCategories.Returns(queryMock);

            _sut = new AddCategoryRequestValidator(_databaseContext);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("longer name")]
        [InlineData("Special characters !@#$%#")]
        public async Task AddCategoryRequestValidator_SuccessIfNameIsOK(string categoryName)
        {
            // Arrange
            var request = new AddCategoryRequest { CategoryName = categoryName };

            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task AddCategoryRequestValidator_FailsIfNameIsEmpty(string categoryName)
        {
            // Arrange
            var request = new AddCategoryRequest { CategoryName = categoryName };

            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddCategoryRequest.CategoryName));
        }

        [Fact]
        public async Task AddCategoryRequestValidator_FailsIfNameAlreadyExists()
        {
            // Arrange
            var request = new AddCategoryRequest { CategoryName = _existingCategories.First().CategoryName };
           
            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddCategoryRequest.CategoryName));
        }

        [Fact]
        public async Task AddCategoryRequestValidator_FailsIfNameAlreadyExistsCaseInsensitive()
        {
            // Arrange
            var request = new AddCategoryRequest { CategoryName = _existingCategories.First().CategoryName.ToUpper() };

            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddCategoryRequest.CategoryName));
        }
    }
}
