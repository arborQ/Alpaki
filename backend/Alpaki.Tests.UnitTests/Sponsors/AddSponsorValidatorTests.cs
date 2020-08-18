using System.Threading.Tasks;
using Alpaki.Logic.Handlers.Sponsors.AddSponsor;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.UnitTests.Sponsors
{
    public class AddSponsorValidatorTests
    {
        private readonly AddSponsorRequest _request = new AddSponsorRequest
        {
            Name = "test",
            Mail = "test@test.com",
            ContactPerson = "test test",
            PhoneNumber = "123456789"
        };
        
        private readonly AddSponsorValidator _sut = new AddSponsorValidator();
        
        [Fact]
        public async Task validation_is_successful_when_request_is_correct()
        {
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeTrue();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        public async Task validation_fails_when_name_is_empty(string name)
        {
            _request.Name = name;
            
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(AddSponsorRequest.Name));
        }

    }
    
}