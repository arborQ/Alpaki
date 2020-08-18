using FluentValidation;

namespace Alpaki.Logic.Handlers.Sponsors.AddSponsor
{
    public class AddSponsorValidator : AbstractValidator<AddSponsorRequest>
    {
        public AddSponsorValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Nazwa sponsora nie może być pusta.");
        }
    }
}