using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic;
using Alpaki.Logic.Handlers.Sponsors.UpdateSponsor;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests.Sponsors
{
    public class UpdateSponsorValidatorTests
    {
        private readonly UpdateSponsorRequest _request = new UpdateSponsorRequest
        {
            Id = 1,
            Name = "test2",
            Mail = "tes2t@test.com",
            ContactPerson = "test2 test2",
            PhoneNumber = "1234567892"
        };

        private readonly IDatabaseContext _dbContext = Substitute.For<IDatabaseContext>();
        private readonly UpdateSponsorValidator _sut;

        public UpdateSponsorValidatorTests()
        {
            var currentUserService = Substitute.For<ICurrentUserService>();
            currentUserService.CurrentUserId.Returns(1);
            currentUserService.CurrentUserRole.Returns(UserRoleEnum.Admin);
            
            _sut = new UpdateSponsorValidator(new UserScopedDatabaseReadContext(_dbContext, currentUserService));

            var sponsors = new List<Sponsor>
            {
                new Sponsor
                {
                    SponsorId = 1,
                    Name = "test",
                    Mail = "test@test.com",
                    ContactPerson = "test test",
                    PhoneNumber = "123456789"
                }
            }.AsQueryable().BuildMockDbSet();
            _dbContext.Sponsors.Returns(sponsors);
        }
        [Fact]
        public async Task validation_is_successful_when_request_is_correct()
        {
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeTrue();
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("    ")]
        public async Task validation_fails_when_name_is_empty(string name)
        {
            _request.Name = name;
            
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(UpdateSponsorRequest.Name));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task validation_fails_when_sponsor_does_not_exit(long sponsorId)
        {
            var sponsors = new List<Sponsor>().AsQueryable().BuildMockDbSet();
            _dbContext.Sponsors.Returns(sponsors);
            _request.Id = sponsorId;
            
            var result = await _sut.ValidateAsync(_request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(UpdateSponsorRequest.Id));
        }

    }
}