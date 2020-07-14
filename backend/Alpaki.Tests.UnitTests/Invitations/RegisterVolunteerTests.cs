using System;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
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
using static Alpaki.Logic.Features.Invitations.Exceptions;
using static Alpaki.Tests.UnitTests.UnitTestHelper;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class RegisterVolunteerTests : UnitTest
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
        private readonly ISystemClock _fakeClock;
        public RegisterVolunteerTests() : base(
            s =>
            {
                s.Replace(ServiceDescriptor.Singleton(Substitute.For<ISystemClock>()));
            })
        {
            _fakeClock = RootProvider.GetService<ISystemClock>();
        }

        [Fact]
        public async Task succeeds_when_there_is_invitation_and_all_register_volunteer_params_are_correct()
        {
            var inviteAVolunteer = new InviteAVolunteerRequest { Email = _validRequest.Email };
            var invitationResponse = await Send(inviteAVolunteer);
            _validRequest.Code = invitationResponse.InvitationCode;

            var registerVolunteerResponse = await Send(_validRequest);

            registerVolunteerResponse.Token.Should().NotBeNullOrWhiteSpace();
            
            var db = RootProvider.GetService<IDatabaseContext>();
            var createdUser = await db.Users.FirstOrDefaultAsync(x => x.UserId == registerVolunteerResponse.UserId);
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
            var inviteAVolunteer = new InviteAVolunteerRequest { Email = _validRequest.Email };
            var _ = await Send(inviteAVolunteer);
            _validRequest.Code = "DIFF";

            Func<Task> action = () => Send(_validRequest);

            await action.Should().ThrowAsync<InvalidInvitationCodeException>();
        }

        [Fact]
        public async Task fails_when_number_of_attempts_is_greater_than_3()
        {
            //given
            var inviteAVolunteer = new InviteAVolunteerRequest { Email = _validRequest.Email };
            var invitationResponse = await Send(inviteAVolunteer);
            _validRequest.Code = "BADC";

            await SuppressException(()=>Send(_validRequest));
            await SuppressException(()=>Send(_validRequest));
            await SuppressException(()=>Send(_validRequest));
            
            //when
            _validRequest.Code = invitationResponse.InvitationCode;
            Func<Task> action = () => Send(_validRequest);

            await action.Should().ThrowAsync<InvitationHasBeenRevokedException>();
        }

        [Fact]
        public async Task fails_when_invitation_has_expired()
        {
            //given there is existing invitation created 24h earlier
            var inviteAVolunteer = new InviteAVolunteerRequest{Email = _validRequest.Email};
            var now = DateTimeOffset.UtcNow;
            _fakeClock.UtcNow.Returns(now - TimeSpan.FromHours(24));
            var invitationResponse = await Send(inviteAVolunteer);
            _validRequest.Code = invitationResponse.InvitationCode;
            _fakeClock.UtcNow.Returns(now + TimeSpan.FromSeconds(1));
            //when
            Func<Task> action = () => Send(_validRequest);

            //then
            await action.Should().ThrowAsync<InvitationHasExpiredException>();
        }

        [Fact]
        public async Task fails_when_there_is_no_pending_invitation_for_given_email()
        {
            Func<Task> action = () => Send(_validRequest);

            await action.Should().ThrowAsync<InvitationNotFoundException>();
        }

        [Fact]
        public async Task fails_when_given_email_is_invalid()
        {
            _validRequest.Email = "invalid";

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Email));
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABC")]
        [InlineData("ABCDE")]
        [InlineData("abcd")]
        [InlineData("!@#$")]
        public async Task fails_when_given_code_is_invalid(string invalidCode)
        {
            _validRequest.Code = invalidCode;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Code));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abcdefg")]
        public async Task fails_when_given_password_is_invalid(string invalidPassword)
        {
            _validRequest.Password = invalidPassword;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Password));
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_first_name_is_invalid(string invalidFirstName)
        {
            _validRequest.FirstName = invalidFirstName;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.FirstName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_last_name_is_invalid(string invalidLastName)
        {
            _validRequest.LastName = invalidLastName;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.LastName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_brand_is_invalid(string invalidBrand)
        {
            _validRequest.Brand = invalidBrand;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Brand));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_phone_number_is_invalid(string invalidPhoneNumber)
        {
            _validRequest.PhoneNumber = invalidPhoneNumber;

            Func<Task> action = () => Send(_validRequest);

            var r = await action.Should().ThrowAsync<ValidationException>();
            r.Which.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.PhoneNumber));
        }
    }
}