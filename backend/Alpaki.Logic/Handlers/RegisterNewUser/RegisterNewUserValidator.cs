using Alpaki.Database;
using FluentValidation;

namespace Alpaki.Logic.Handlers.RegisterNewUser
{
    public class RegisterNewUserRequestValidator : AbstractValidator<RegisterNewUserRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public RegisterNewUserRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
