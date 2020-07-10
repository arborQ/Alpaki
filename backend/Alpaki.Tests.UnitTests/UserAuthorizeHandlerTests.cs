using System;
using Alpaki.Logic.Services;
using NSubstitute;
using Xunit;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using AutoFixture;
using Alpaki.Database;
using FluentValidation.Results;
using System.Collections.Generic;
using Alpaki.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using MockQueryable.NSubstitute;
using System.Linq.Expressions;

namespace Alpaki.Tests.UnitTests
{
    public class UserAuthorizeHandlerTests
    {
        private readonly Fixture _fixture;

        public UserAuthorizeHandlerTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("login", "")]
        [InlineData("", "password")]
        public async Task UserAuthorizeHandler_ShouldValidateLoginAndPassword_ThrowException_IfValidatorReturnFail(string login, string password)
        {
            // Arrange
            var validatorMock = Substitute.For<IValidator<UserAuthorizeRequest>>();
            var databaseContextMock = Substitute.For<IDatabaseContext>();

            var userDb = new List<User>().AsQueryable().BuildMockDbSet();
            databaseContextMock.Users.Returns(userDb);

            validatorMock.ValidateAsync(Arg.Any<UserAuthorizeRequest>()).Returns(Task.FromResult(new ValidationResult(_fixture.CreateMany<ValidationFailure>(2))));

            var sut = new UserAuthorizeHandler(validatorMock, databaseContextMock);
            var request = _fixture
                .Build<UserAuthorizeRequest>()
                .With(a => a.Login, login)
                .With(a => a.Password, password)
                .Create();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var response = await sut.Handle(request, CancellationToken.None);
            });

            // Assert
            await validatorMock.Received().ValidateAsync(Arg.Any<UserAuthorizeRequest>());
        }
    }
}
