using FluentValidation;
using static Alpaki.Logic.Handlers.AddCategory.AddCategoryRequest;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class CategoryDefaultStepValidator : AbstractValidator<CategoryDefaultStep>
    {
        public CategoryDefaultStepValidator()
        {
            RuleFor(r => r.StepDescription).NotEmpty().WithMessage("Nazwa kroku musi jest wymagana");
            RuleFor(r => r.StepDescription).MaximumLength(250).WithMessage("Nazwa kroku nie może być dłuższa niż 250");
        }
    }
}
