using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Internal;
using NSubstitute;
using Xunit;
using static Alpaki.Logic.Features.Invitations.Exceptions;
using static Alpaki.Tests.UnitTests.UnitTestHelper;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class RegisterVolunteerTests
    {
        private readonly RegisterVolunteer _validRequest = new RegisterVolunteer
        {
            Email = "test@test.com",
            Code = "1A2B",
            Password = "Test1234",
            PhoneNumber = "123456789",
            FirstName = "Test",
            LastName = "Test",
            Brand = "Test"
        };

        private readonly RegisterVolunteerHandler _handler;
        private readonly ISystemClock _fakeClock;
        private readonly FakeInvitationRepository _fakeInvitationRepository;
        private readonly FakeVolunteerRepository _fakeVolunteerRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterVolunteerTests()
        {
            _fakeClock = Substitute.For<ISystemClock>();
            var jwtGenerator = Substitute.For<IJwtGenerator>();
            jwtGenerator.Generate(Arg.Any<User>()).Returns("something");
            _fakeInvitationRepository = new FakeInvitationRepository();
            _fakeVolunteerRepository = new FakeVolunteerRepository();
            _passwordHasher = Substitute.For<IPasswordHasher<User>>();
            _handler = new RegisterVolunteerHandler(jwtGenerator, _fakeClock, _fakeInvitationRepository, _fakeVolunteerRepository, _passwordHasher);
        }

        [Fact]
        public async Task succeeds_when_there_is_invitation_and_all_register_volunteer_params_are_correct()
        {
           await _fakeInvitationRepository.AddAsync(
               new Invitation
               {
                   Email = _validRequest.Email,
                   Code = _validRequest.Code,
                   Status = InvitationStateEnum.Pending,
                   Attempts = 0,
                   CreatedAt = _fakeClock.UtcNow,
                   InvitationId = 1
               },
               CancellationToken.None
           );

            var registerVolunteerResponse = await _handler.Handle(_validRequest, CancellationToken.None);
          
            registerVolunteerResponse.Token.Should().NotBeNullOrWhiteSpace();
          
            var createdUser = _fakeVolunteerRepository.Get(registerVolunteerResponse.UserId);
            createdUser.Should().NotBeNull();
            createdUser.Email.Should().BeEquivalentTo(_validRequest.Email);
            createdUser.FirstName.Should().BeEquivalentTo(_validRequest.FirstName);
            createdUser.LastName.Should().BeEquivalentTo(_validRequest.FirstName);
            createdUser.Role.Should().BeEquivalentTo(UserRoleEnum.Volunteer);
            createdUser.PhoneNumber.Should().BeEquivalentTo(_validRequest.PhoneNumber);
            createdUser.Brand.Should().BeEquivalentTo(_validRequest.Brand);
            createdUser.PasswordHash.Should().NotBe(_validRequest.Password);
        }

        [Fact]
        public async Task fails_when_given_code_does_not_match_one_in_invitation()
        {
            await _fakeInvitationRepository.AddAsync(new Invitation { Email = _validRequest.Email, Code = _validRequest.Code, Status = InvitationStateEnum.Pending, Attempts = 0, CreatedAt = _fakeClock.UtcNow, InvitationId = 1 }, CancellationToken.None);

            _validRequest.Code = "DIFF";

            Func<Task> action = () => _handler.Handle(_validRequest, CancellationToken.None);

            await action.Should().ThrowAsync<InvalidInvitationCodeException>();
        }

        [Fact]
        public async Task fails_when_number_of_attempts_is_greater_than_3()
        {
            await _fakeInvitationRepository.AddAsync(new Invitation { Email = _validRequest.Email, Code = _validRequest.Code, Status = InvitationStateEnum.Pending, Attempts = 0, CreatedAt = _fakeClock.UtcNow, InvitationId = 1 }, CancellationToken.None);
            var validCode = _validRequest.Code;
            _validRequest.Code = "BADC";

            await SuppressException(()=> _handler.Handle(_validRequest, CancellationToken.None));
            await SuppressException(()=> _handler.Handle(_validRequest, CancellationToken.None));
            await SuppressException(()=> _handler.Handle(_validRequest, CancellationToken.None));
            
            _validRequest.Code = validCode;
            Func<Task> action = () => _handler.Handle(_validRequest, CancellationToken.None);

            await action.Should().ThrowAsync<InvitationHasBeenRevokedException>();
        }

        [Fact]
        public async Task fails_when_invitation_has_expired()
        {
            var now = DateTimeOffset.UtcNow;
            _fakeClock.UtcNow.Returns(now);
            await _fakeInvitationRepository.AddAsync(new Invitation { Email = _validRequest.Email, Code = _validRequest.Code, Status = InvitationStateEnum.Pending, Attempts = 0, CreatedAt = now - TimeSpan.FromHours(24), InvitationId = 1 }, CancellationToken.None);
            _fakeClock.UtcNow.Returns(now + TimeSpan.FromSeconds(1));

            Func<Task> action = () => _handler.Handle(_validRequest, CancellationToken.None);

            await action.Should().ThrowAsync<InvitationHasExpiredException>();
        }

        [Fact]
        public async Task fails_when_there_is_no_pending_invitation_for_given_email()
        {
            Func<Task> action = () => _handler.Handle(_validRequest, CancellationToken.None);

            await action.Should().ThrowAsync<InvitationNotFoundException>();
        }

    }
}