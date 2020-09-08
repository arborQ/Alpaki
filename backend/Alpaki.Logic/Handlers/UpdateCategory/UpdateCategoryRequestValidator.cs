using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateCategory
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public UpdateCategoryRequestValidator(IDatabaseContext databaseContext, UpdateCategoryDefaultStepValidator stepValidator)
        {
            RuleFor(r => r.CategoryId).MustAsync(CategoryExists).WithMessage(r => $"Kategoria o Id=[{r.CategoryId}] nie istnieje");
            RuleFor(r => r.CategoryName).NotEmpty().When(r => r.CategoryName != null).WithMessage("Kategoria nie może mieć pustej nazwy");
            RuleFor(r => r.CategoryName).MaximumLength(250).When(r => r.CategoryName != null).WithMessage("Nazwa kategorii nie może być dłuższa niż 250 znaków");
            RuleFor(r => r).MustAsync((c, cancellationToken) => CategoryNameDoesNotExists(c.CategoryName, c.CategoryId, cancellationToken)).When(c => c.CategoryName != null).WithMessage("Taka kategoria już istnieje");
            RuleForEach(r => r.DefaultSteps).SetValidator(stepValidator);

            _databaseContext = databaseContext;
        }

        private Task<bool> CategoryExists(long categoryId, CancellationToken cancellationToken)
        {
            return _databaseContext.DreamCategories
                .AnyAsync(d => d.DreamCategoryId == categoryId, cancellationToken: cancellationToken);
        }

        private Task<bool> CategoryNameDoesNotExists(string categoryName, long categoryId, CancellationToken cancellationToken)
        {
            return _databaseContext.DreamCategories
                .AllAsync(d => d.DreamCategoryId == categoryId || d.CategoryName != categoryName, cancellationToken: cancellationToken);
        }

    }
}
