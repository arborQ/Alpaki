using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserRequestValidator: AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator(IUserScopedDatabaseReadContext userScopedDatabaseReadContext)
        {
            RuleFor(r => r.UserId)
                .MustAsync((userId,c) => userScopedDatabaseReadContext.Users.AnyAsync(u => u.UserId == userId, c))
                .WithMessage(r => $"Użytkownik Id=[{r.UserId}] nie istnieje");
        }
    }
}
