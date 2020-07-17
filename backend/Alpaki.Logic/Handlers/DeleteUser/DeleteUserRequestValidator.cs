using Alpaki.CrossCutting.Interfaces;
using FluentValidation;

namespace Alpaki.Logic.Handlers.DeleteUser
{
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator(ICurrentUserService currentUserService)
        {
            RuleFor(r => r.UserId).Must(userId => userId != currentUserService.CurrentUserId);
        }
    }
}
