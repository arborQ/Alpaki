using System.Threading;
using System.Threading.Tasks;
using Alpaki.Logic.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateDream
{
    public class UpdateDreamRequestValidator : AbstractValidator<UpdateDreamRequest>
    {
        private readonly IUserScopedDatabaseReadContext _dbContext;

        public UpdateDreamRequestValidator(IUserScopedDatabaseReadContext dbContext, IImageIdValidator imageValidator)
        {
            RuleFor(x => x.DreamId).NotNull();
            RuleFor(x => x.DreamId).MustAsync(DreamExists);
            RuleFor(x => x.DisplayName).NotEmpty().When(x => x.DisplayName != null).WithMessage("Nazwa nie może być pusta.");
            RuleFor(d => d.Age).GreaterThan(0).LessThan(121).WithMessage("Wiek pomiędzy 1 a 120 lat");

            RuleFor(x => x.DreamUrl).NotEmpty().When(x => x.DreamUrl != null).WithMessage("Link do marzenia nie może być pusty.");
            RuleFor(x => x.Tags).NotEmpty().When(x => x.Tags != null).WithMessage("Tagi nie mogą być puste.");
            RuleFor(x => x.CategoryId).MustAsync(DreamCategoryExists).When(x => x.CategoryId != null).WithMessage(r => $"Kategoria o Id=[{r.CategoryId}] nie istnieje");
            RuleFor(x => x.DreamImageId).MustAsync(imageValidator.ImageIdIsAvailable).When(a => a.DreamImageId.HasValue).WithMessage("Obraz jest już przypisany do innego elementu.");

            _dbContext = dbContext;
        }

        private async Task<bool> DreamExists(long dreamId, CancellationToken cancellationToken)
            => await _dbContext.Dreams.AnyAsync(x => x.DreamId == dreamId, cancellationToken: cancellationToken);

        private async Task<bool> DreamCategoryExists(long? dreamCategoryId, CancellationToken cancellationToken)
            => await _dbContext.DreamCategories.AnyAsync(x => x.DreamCategoryId == dreamCategoryId, cancellationToken: cancellationToken);
    }
}