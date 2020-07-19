using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Handlers.ChangeUserRole;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.UnitTests.ChangeUserRole
{
    public class ChangeUserRoleValidatorTests
    {
        [Theory]
        [InlineData(UserRoleEnum.Admin)]
        [InlineData(UserRoleEnum.Coordinator)]
        public async Task accepts_role(UserRoleEnum role)
        {
            var sut = new ChangeUserRoleValidator();

            var result = await sut.ValidateAsync(
                new ChangeUserRoleRequest
                {
                    UserId = 1,
                    Role = role
                }
            );

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(UserRoleEnum.None)]
        [InlineData(UserRoleEnum.Volunteer)]
        public async Task fails_on_role(UserRoleEnum role)
        {
            var sut = new ChangeUserRoleValidator();

            var result = await sut.ValidateAsync(
                new ChangeUserRoleRequest
                {
                    UserId = 1,
                    Role = role
                }
            );

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(ChangeUserRoleRequest.Role));

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task fails_on_invalid_userId(int userId)
        {
            var sut = new ChangeUserRoleValidator();

            var result = await sut.ValidateAsync(
                new ChangeUserRoleRequest
                {
                    UserId = userId,
                    Role = UserRoleEnum.Admin
                }
            );

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(ChangeUserRoleRequest.UserId));
        }
    }
}