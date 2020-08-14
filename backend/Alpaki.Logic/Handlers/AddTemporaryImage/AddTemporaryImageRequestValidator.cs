using FluentValidation;

namespace Alpaki.Logic.Handlers.AddTemporaryImage
{
    public class AddTemporaryImageRequestValidator : AbstractValidator<AddTemporaryImageRequest>
    {
        public AddTemporaryImageRequestValidator()
        {
            RuleFor(r => r.ImageData).NotEmpty().WithMessage("Plik jest wymagany");
        }
    }
}
