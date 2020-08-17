using FluentValidation;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerRequestValidator : AbstractValidator<CreateDreamerRequest>
    {
        public CreateDreamerRequestValidator()
        {
            RuleFor(d => d.DisplayName).NotEmpty().WithMessage("Nazwa jest wymagana");
            RuleFor(d => d.Age).GreaterThan(0).LessThan(121).WithMessage("Wiek pomiędzy 1 a 120 lat");
        }
    }
}
