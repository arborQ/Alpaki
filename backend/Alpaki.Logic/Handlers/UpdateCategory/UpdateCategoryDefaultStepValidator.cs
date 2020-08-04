using FluentValidation;
using static Alpaki.Logic.Handlers.UpdateCategory.UpdateCategoryRequest;

namespace Alpaki.Logic.Handlers.UpdateCategory
{
    public class UpdateCategoryDefaultStepValidator : AbstractValidator<UpdateCategoryDefaultStep>
    {
        public UpdateCategoryDefaultStepValidator()
        {
            RuleFor(r => r.StepDescription).NotEmpty().WithMessage("Nazwa kroku musi jest wymagana");
            RuleFor(r => r.StepDescription).MaximumLength(250).WithMessage("Nazwa kroku nie może być dłuższa niż 250");
        }
    }
}
