using System;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Logic.Features.Invitations;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class InviteAVolunteerTests : UnitTest, IDisposable
    {
        private readonly InviteAVolunteerRequest _validRequest = new InviteAVolunteerRequest{Email = "test@test.com"};
        private readonly ISystemClock _fakeClock;

        public InviteAVolunteerTests()
            :base(x=>x.Replace(ServiceDescriptor.Singleton(Substitute.For<ISystemClock>())))
        {
            _fakeClock = RootProvider.GetService<ISystemClock>();
        }
        
        [Fact]
        public async Task creates_new_invitation_when_there_are_not_any_invitation_with_given_email_and_email_is_correct()
        {
            var now = DateTimeOffset.UtcNow;
            _fakeClock.UtcNow.Returns(now);
            var response = await Send(_validRequest);

            response.InvitationCode.Should().MatchRegex("[0-9A-Z]{4}");
            
            var db = RootProvider.GetService<IDatabaseContext>();
            var invitation = await db.Invitations.AsNoTracking().SingleOrDefaultAsync(x=>x.InvitationId==response.InvitationId);
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
            var response = await Send(_validRequest);
            var db = RootProvider.GetService<IDatabaseContext>();
            var invitation = await db.Invitations.AsNoTracking().SingleOrDefaultAsync(x => x.InvitationId == response.InvitationId);

            _fakeClock.UtcNow.Returns(now + TimeSpan.FromHours(1));
            var response2 = await Send(_validRequest);
            var invitation2 = await db.Invitations.AsNoTracking().SingleOrDefaultAsync(x => x.InvitationId == response2.InvitationId);

            response.InvitationId.Should().Be(response2.InvitationId);
            response.InvitationCode.Should().NotBe(response2.InvitationCode);

            (invitation2.CreatedAt - invitation.CreatedAt).Should().Be(TimeSpan.FromHours(1));
        }

        [Fact]
        public async Task fails_when_there_is_volunteer_with_given_email()
        {
            var response = await Send(_validRequest);
            await Send(
                new RegisterVolunteer
                {
                    Email = _validRequest.Email, Code = response.InvitationCode,
                    Password = "test1234",
                    PhoneNumber = "123456789",
                    Brand = "test",
                    LastName = "test",
                    FirstName = "test"
                }
            );

            Func<Task> action = () => Send(_validRequest);

            await action.Should().ThrowAsync<Exceptions.VolunteerAlreadyExistsException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        public async Task fails_when_given_email_has_invalid_form(string invalidEmail)
        {
            _validRequest.Email = invalidEmail;
            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(InviteAVolunteerRequest.Email));
        }

        public void Dispose()
        {
            
        }
    }
}