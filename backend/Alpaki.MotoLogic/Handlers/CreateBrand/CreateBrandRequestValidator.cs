using FluentValidation;

namespace Alpaki.MotoLogic.Handlers.CreateBrand
{
    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(b => b.BrandName).MaximumLength(500);
        }
    }
}
