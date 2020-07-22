using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class RegisterVolunteerValidatorTests
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

        private readonly RegisterVolunteerValidator _validator;
        public RegisterVolunteerValidatorTests()
        {
            _validator = new RegisterVolunteerValidator();
        }

        [Fact]
        public async Task succeeds_when_given_request_is_valid()
        {
            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        public async Task fails_when_given_email_has_invalid_form(string invalidEmail)
        {
            _validRequest.Email = invalidEmail;
            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Email));
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

            var result = await _validator.ValidateAsync(_validRequest);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Code));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abcdefg")]
        public async Task fails_when_given_password_is_invalid(string invalidPassword)
        {
            _validRequest.Password = invalidPassword;

            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Password));
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_first_name_is_invalid(string invalidFirstName)
        {
            _validRequest.FirstName = invalidFirstName;

            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.FirstName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_last_name_is_invalid(string invalidLastName)
        {
            _validRequest.LastName = invalidLastName;

            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.LastName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_brand_is_invalid(string invalidBrand)
        {
            _validRequest.Brand = invalidBrand;

            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.Brand));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task fails_when_given_phone_number_is_invalid(string invalidPhoneNumber)
        {
            _validRequest.PhoneNumber = invalidPhoneNumber;

            var result = await _validator.ValidateAsync(_validRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(RegisterVolunteer.PhoneNumber));
        }
    }
}