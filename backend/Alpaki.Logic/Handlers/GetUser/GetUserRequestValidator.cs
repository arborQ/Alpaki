using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator(IUserScopedDatabaseReadContext userScopedDatabaseReadContext)
        {
            RuleFor(r => r.UserId)
                .MustAsync((userId, cancellationToken) => userScopedDatabaseReadContext.Users.AnyAsync(u => u.UserId == userId, cancellationToken))
                .WithMessage(r => $"Użytkownik UserId=[{r.UserId}] nie istnieje");
        }
    }
}
