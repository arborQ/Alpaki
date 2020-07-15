using Alpaki.Database;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteerValidator : AbstractValidator<RegisterVolunteer>
    {
        public RegisterVolunteerValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Podany email jest niepoprawny.");
            RuleFor(x => x.Code).NotEmpty().Length(4).Matches("[0-9A-Z]{4}").WithMessage("Podany kod jest niepoprawny.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Podane hasło jest za krótkie.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Numer telefonu jest wymagany.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Imię jest wymagane.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Nazwisko jest wymagane.");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Oddział jest wymagany.");
        }
    }
}