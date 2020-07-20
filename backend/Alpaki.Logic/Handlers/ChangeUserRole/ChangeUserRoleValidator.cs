using System.Linq;
using Alpaki.CrossCutting.Enums;
using FluentValidation;

namespace Alpaki.Logic.Handlers.ChangeUserRole
{
    public class ChangeUserRoleValidator : AbstractValidator<ChangeUserRoleRequest>
    {
        public ChangeUserRoleValidator()
        {
            RuleFor(x => x.Role).IsInEnum()
                .Must(x => new[] {UserRoleEnum.Admin, UserRoleEnum.Coordinator}.Contains(x))
                .WithMessage($"Podano niepoprawną role. Dostępne role to: administrator({(int)UserRoleEnum.Admin}) i koordynator({(int)UserRoleEnum.Coordinator}).");
        }
    }
}