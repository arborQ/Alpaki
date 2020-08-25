using FluentValidation;
using FluentValidation.Validators;

namespace Alpaki.Logic.Handlers.AddSponsor
{
    public class AddSponsorRequestValidator : AbstractValidator<AddSponsorRequest>
    {
        public AddSponsorRequestValidator()
        {
            RuleFor(r => r.DisplayName).NotEmpty().MaximumLength(500).WithMessage("Wyświetlana nazwa jest wymagana i nie może być dłuższa niz 500 znaków");
            RuleFor(r => r.ContactPersonName).NotEmpty().MaximumLength(500).WithMessage("Osoba kontaktowa jest wymagana i nie może być dłuższa niz 500 znaków");
            RuleFor(r => r.Email).NotEmpty().EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Podany email jest niepoprawny.");
            RuleFor(r => r.Brand).MaximumLength(250).WithMessage("Nazwa oddziału nie możę być dłuższa niż 250 znaków");
        }
    }
}
