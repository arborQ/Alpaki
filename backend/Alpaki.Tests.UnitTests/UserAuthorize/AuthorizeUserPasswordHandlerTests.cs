using System;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using AutoFixture;
using Alpaki.Database;
using Alpaki.Database.Models;
using System.Linq;
using MockQueryable.NSubstitute;
using Alpaki.Logic.Handlers.AuthorizeUserPassword;
using Alpaki.CrossCutting.Interfaces;
using FluentAssertions;
using Alpaki.Logic;
using Alpaki.Tests.Common.Builders;
using Microsoft.AspNetCore.Identity;

namespace Alpaki.Tests.UnitTests.UserAuthorize
{
    public class AuthorizeUserPasswordHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly AuthorizeUserPasswordHandler _sut;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IDatabaseContext _databaseContext;

        public AuthorizeUserPasswordHandlerTests()
        {
            _fixture = new Fixture();
            _passwordHasher = Substitute.For<IPasswordHasher<User>>();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _databaseContext = Substitute.For<IDatabaseContext>();
            _sut = new AuthorizeUserPasswordHandler(_passwordHasher, _jwtGenerator, _databaseContext);
        }

        [Fact]
        public async Task AuthorizeUserPasswordHandler_ValidatePassword_AgainstUserHashedPassword()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var token = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);
            var usersList = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);
            _passwordHasher.VerifyHashedPassword(null,Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);
            _jwtGenerator.Generate(Arg.Any<User>()).Returns(token);
            var usedUser = users.First();

            // Act
            var result = await _sut.Handle(new AuthorizeUserPasswordRequest { Login = usedUser.Email, Password = password }, default);

            // Assert
            result.Token.Should().NotBeNullOrWhiteSpace();
            result.Token.Should().Be(token);
            _passwordHasher.Received(1).VerifyHashedPassword(null,Arg.Any<string>(), Arg.Any<string>());
            _passwordHasher.Received(1).VerifyHashedPassword(null,usedUser.PasswordHash, password);
            _jwtGenerator.Received(1).Generate(Arg.Any<User>());
            _jwtGenerator.Received(1).Generate(Arg.Is<User>(u => u.UserId == usedUser.UserId && u.Role == usedUser.Role));
        }

        [Fact]
        public async Task AuthorizeUserPasswordHandler_ThrowsException_IfInvalidPassword()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);
            var usersList = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);
            _passwordHasher.VerifyHashedPassword(null,Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Failed);

            var usedUser = users.First();

            // Act
            await Assert.ThrowsAnyAsync<LogicException>(() => _sut.Handle(new AuthorizeUserPasswordRequest { Login = usedUser.Email, Password = password }, default));

            // Assert
            _jwtGenerator.DidNotReceive().Generate(Arg.Any<User>());
        }

        [Fact]
        public async Task AuthorizeUserPasswordHandler_ThrowsException_IfInvalidUserEmail()
        {
            // Arrange
            var password = _fixture.Create<string>();
            var users = _fixture.VolunteerBuilder().WithPassword(password).CreateMany(10);
            var usersList = users.AsQueryable().BuildMockDbSet();
            _databaseContext.Users.Returns(usersList);
            _passwordHasher.VerifyHashedPassword(null, Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Failed);

            var usedUser = users.First();

            // Act
            await Assert.ThrowsAnyAsync<Exception>(() => _sut.Handle(new AuthorizeUserPasswordRequest { Login = _fixture.Create<string>(), Password = password }, default));

            // Assert
            _jwtGenerator.DidNotReceive().Generate(Arg.Any<User>());
        }
    }
}
