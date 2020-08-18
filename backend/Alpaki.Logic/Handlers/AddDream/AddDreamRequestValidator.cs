using Alpaki.Logic.Validators;
using FluentValidation;

namespace Alpaki.Logic.Handlers.AddDream
{
    public class AddDreamRequestValidator : AbstractValidator<AddDreamRequest>
    {
        public AddDreamRequestValidator(IImageIdValidator imageValidator)
        {
            RuleFor(d => d.DisplayName).NotEmpty().WithMessage("Nazwa jest wymagana");
            RuleFor(d => d.Age).GreaterThan(0).LessThan(121).WithMessage("Wiek pomiędzy 1 a 120 lat");
            RuleFor(x => x.DreamImageId).MustAsync(imageValidator.ImageIdIsAvailable).When(a => a.DreamImageId.HasValue).WithMessage("Obraz jest już przypisany do innego elementu.");
        }
    }
}
