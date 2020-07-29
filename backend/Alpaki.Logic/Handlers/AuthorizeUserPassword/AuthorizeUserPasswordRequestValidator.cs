using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class AuthorizeUserPasswordRequestValidator : AbstractValidator<AuthorizeUserPasswordRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public AuthorizeUserPasswordRequestValidator(IDatabaseContext databaseContext)
        {
            RuleFor(u => u.Login).MustAsync(UserWithGivenEmailExists).WithMessage(u => $"Użytkownik {u.Login} nie istnieje");
            _databaseContext = databaseContext;
        }

        private Task<bool> UserWithGivenEmailExists(string email, CancellationToken cancellationToken) => _databaseContext.Users.AnyAsync(u => u.Email == email);
    }
}
