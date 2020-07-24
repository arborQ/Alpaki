using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Invitations;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Internal;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class InviteAVolunteerTests
    {
        private readonly InviteAVolunteerRequest _validRequest = new InviteAVolunteerRequest{Email = "test@test.com"};
        private readonly ISystemClock _fakeClock;
        private readonly FakeInvitationRepository _fakeInvitationRepository;
        private readonly FakeVolunteerRepository _fakeVolunteerRepository;
        private readonly IMediator _mediator;
        private readonly InviteAVolunteerHandler _handler;
        public InviteAVolunteerTests()
        {
            _fakeClock = Substitute.For<ISystemClock>();
            _fakeInvitationRepository = new FakeInvitationRepository();
            _fakeVolunteerRepository = new FakeVolunteerRepository();
            _mediator = Substitute.For<IMediator>();

            _handler = new InviteAVolunteerHandler(new InvitationCodesGenerator(), _mediator, _fakeClock, _fakeInvitationRepository, _fakeVolunteerRepository);
        }
        
        [Fact]
        public async Task creates_new_invitation_when_there_are_not_any_invitation_with_given_email_and_email_is_correct()
        {
            var now = DateTimeOffset.UtcNow;
            _fakeClock.UtcNow.Returns(now);
            var response = await _handler.Handle(_validRequest, CancellationToken.None);

            response.InvitationCode.Should().MatchRegex("[0-9A-Z]{4}");

            var invitation = _fakeInvitationRepository.Get(response.InvitationId);
            invitation.Attempts.Should().Be(0);
            invitation.Code.Should().BeEquivalentTo(response.InvitationCode);
            invitation.Status.Should().Be(InvitationStateEnum.Pending);
            invitation.Email.Should().BeEquivalentTo(_validRequest.Email);
            invitation.CreatedAt.Should().Be(_fakeClock.UtcNow);
        }

        [Fact]
        public async Task refreshes_existing_invitation_when_request_is_repeated()
        {
            var now = DateTimeOffset.UtcNow;
            _fakeClock.UtcNow.Returns(now);
            var response = await _handler.Handle(_validRequest, CancellationToken.None);
            var invitation = _fakeInvitationRepository.Get(response.InvitationId);

            _fakeClock.UtcNow.Returns(now + TimeSpan.FromHours(1));
            var response2 = await _handler.Handle(_validRequest, CancellationToken.None);
            var invitation2 = _fakeInvitationRepository.Get(response2.InvitationId);

            response.InvitationId.Should().Be(response2.InvitationId);
            response.InvitationCode.Should().NotBe(response2.InvitationCode);

            (invitation2.CreatedAt - invitation.CreatedAt).Should().Be(TimeSpan.FromHours(1));
        }

        [Fact]
        public async Task fails_when_there_is_volunteer_with_given_email()
        {
            await _fakeVolunteerRepository.AddAsync(
                new User
                {
                    Email = _validRequest.Email, 
                    PhoneNumber = "123456789", 
                    Brand = "test", 
                    FirstName = "test", 
                    LastName = "test",
                    Role = UserRoleEnum.Volunteer, 
                    PasswordHash = "some_hash", 
                    UserId = 1
                },
                CancellationToken.None
            );

            Func<Task> action = () => _handler.Handle(_validRequest, CancellationToken.None);

            await action.Should().ThrowAsync<Exceptions.VolunteerAlreadyExistsException>();
        }

    }
}