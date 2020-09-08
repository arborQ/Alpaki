using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public AddCategoryRequestValidator(IDatabaseContext databaseContext, CategoryDefaultStepValidator stepValidator)
        {
            RuleFor(r => r.CategoryName).NotEmpty().WithMessage("Kategoria nie może mieć pustej nazwy");
            RuleFor(r => r.CategoryName).MaximumLength(250).WithMessage("Nazwa kategorii nie może być dłuższa niż 250 znaków");
            RuleFor(r => r.CategoryName).MustAsync(CategoryDoesNotExists).When(c => !string.IsNullOrWhiteSpace(c.CategoryName)).WithMessage("Taka kategoria już istnieje");
            RuleFor(r => r.DefaultSteps).NotEmpty().WithMessage("Kategoria musi zawierać domyślne kroki");
            RuleForEach(r => r.DefaultSteps).SetValidator(stepValidator);

            _databaseContext = databaseContext;
        }
        private Task<bool> CategoryDoesNotExists(string categoryName, CancellationToken cancellationToken)
        {
            return _databaseContext.DreamCategories.AllAsync(d => d.CategoryName.ToLower() != categoryName.ToLower(), cancellationToken: cancellationToken);
        }
    }
}
