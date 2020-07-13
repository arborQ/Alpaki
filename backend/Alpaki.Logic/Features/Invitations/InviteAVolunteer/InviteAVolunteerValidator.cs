using FluentValidation;
using FluentValidation.Validators;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerValidator : AbstractValidator<InviteAVolunteerRequest>
    {
        public InviteAVolunteerValidator()
        {
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email nie jest poprawny.");
        }
    }
}