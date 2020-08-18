using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic;
using Alpaki.Logic.Handlers.UpdateUserData;
using Alpaki.Logic.Validators;
using Alpaki.Tests.IntegrationTests.Fixtures.Builders;
using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Logic.Validators
{
    public class UpdateUserDataRequestValidatorTests
    {
        private readonly Fixture _fixture;
        private readonly UpdateUserDataRequestValidator _sut;
        private readonly IUserScopedDatabaseReadContext _databaseContext;
        private readonly IDatabaseContext _fullDatabaseContext;
        private readonly IImageIdValidator _imageIdValidator = Substitute.For<IImageIdValidator>();

        public UpdateUserDataRequestValidatorTests()
        {
            _fixture = new Fixture();
            _databaseContext = Substitute.For<IUserScopedDatabaseReadContext>();
            _fullDatabaseContext = Substitute.For<IDatabaseContext>();
            _imageIdValidator.ImageIdIsAvailable(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            _sut = new UpdateUserDataRequestValidator(_databaseContext, _fullDatabaseContext, _imageIdValidator);
        }

        [Fact]
        public async Task UpdateUserDataRequestValidator_DoesNotReturnError_IfPropertyIsNull()
        {
            // Arrange
            var users = _fixture.VolunteerBuilder().With(u => u.UserId, () => _fixture.Create<long>()).CreateMany(10).ToList();
            var request = new UpdateUserDataRequest { UserId = users.OrderBy(_ => new Random().Next()).First().UserId };
            
            var userMock = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(userMock);

            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task UpdateUserDataRequestValidator_DoesReturnError_IfPropertyIsInvalid()
        {
            // Arrange
            var users = _fixture.VolunteerBuilder().With(u => u.UserId, () => _fixture.Create<long>()).CreateMany(10).ToList();
            var userMock = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(userMock);

            var request = new UpdateUserDataRequest
            {
                FirstName = "",
                LastName = "",
                Brand = "",
                Email = "not email",
                PhoneNumber = ""
            };

            // Act
            var result = await _sut.ValidateAsync(request, default);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.UserId));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.FirstName));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.LastName));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.Brand));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.Email));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(User.PhoneNumber));
        }

    }
}
