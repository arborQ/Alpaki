using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic;
using Alpaki.Logic.Handlers.UpdateDream;
using Alpaki.Logic.Validators;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Validators
{
    public class UpdateDreamerRequestValidatorTests
    {
        private readonly UpdateDreamRequest _request = new UpdateDreamRequest
        {
            DreamId = 1,
            DisplayName = "test",
            Age = 1,
            DreamUrl = "https://mam-marzenie.pl/marzenie/1",
            Tags = "tag1",
            CategoryId = 1
        };

        private readonly IDatabaseContext _dbContext = Substitute.For<IDatabaseContext>();
        private readonly IImageIdValidator _imageIdValidator = Substitute.For<IImageIdValidator>();
        private readonly UpdateDreamRequestValidator _sut;

        public UpdateDreamerRequestValidatorTests()
        {
            var currentUserService = Substitute.For<ICurrentUserService>();
            currentUserService.CurrentUserId.Returns(1);
            currentUserService.CurrentUserRole.Returns(UserRoleEnum.Admin);
            _imageIdValidator.ImageIdIsAvailable(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _sut = new UpdateDreamRequestValidator(new UserScopedDatabaseReadContext(_dbContext, currentUserService), _imageIdValidator);
            var categories = new List<DreamCategory>
            {
                new DreamCategory
                {
                    DreamCategoryId = 1,
                    CategoryName = "test"
                }
            }.AsQueryable().BuildMockDbSet();
            var dreams = new List<Dream>
            {
                new Dream
                {
                    DreamId = 1,
                    DisplayName = "test",
                    Age = 1,
                    DreamUrl = "https://mam-marzenie.pl/marzenie/1",
                    Tags = "tag1",
                    DreamCategoryId = 1
                }
            }.AsQueryable().BuildMockDbSet();
            _dbContext.DreamCategories.Returns(categories);
            _dbContext.Dreams.Returns(dreams);
        }

        [Fact]
        public async Task success_when_request_is_valid()
        {
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeTrue();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        public async Task fails_when_first_name_is_blank(string firstName)
        {
            _request.DisplayName = firstName;

            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateDreamRequest.DisplayName));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        public async Task fails_when_last_name_is_blank(string lastName)
        {
            _request.DisplayName = lastName;

            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateDreamRequest.DisplayName));
        }
        
        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(121)]
        [InlineData(122)]
        public async Task fails_when_age_is_not_in_valid_Range(int age)
        {
            _request.Age = age;

            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateDreamRequest.Age));
        }
        
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        
        public async Task fails_when_given_dream_category_does_not_exits(long dreamCategoryId)
        {
            _request.CategoryId = dreamCategoryId;

            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateDreamRequest.CategoryId));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]

        public async Task fails_when_given_dream_does_not_exits(long dreamId)
        {
            _request.DreamId = dreamId;

            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateDreamRequest.DreamId));
        }
    }
}