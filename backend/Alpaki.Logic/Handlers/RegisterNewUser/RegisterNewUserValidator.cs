using Alpaki.Database;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.RegisterNewUser
{
    public class RegisterNewUserRequestValidator : AbstractValidator<RegisterNewUserRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public RegisterNewUserRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(250);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(250);
            RuleFor(r => r.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Podany email jest niepoprawny.");
            RuleFor(r => r.Email).MustAsync((e, c) => _databaseContext.Users.AllAsync(u => u.Email != e, c));
            RuleFor(r => r.Brand).NotEmpty().MaximumLength(250);
        }
    }
}
