using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateDreamer
{
    public class UpdateDreamerRequestValidator : AbstractValidator<UpdateDreamerRequest>
    {
        private readonly IUserScopedDatabaseReadContext _dbContext;

        public UpdateDreamerRequestValidator(IUserScopedDatabaseReadContext dbContext)
        {
            RuleFor(x => x.DreamId).NotNull();
            RuleFor(x => x.DreamId).MustAsync(DreamExists);
            RuleFor(x => x.FirstName).NotEmpty().When(x => x.FirstName is {}).WithMessage("Imię nie może być puste.");
            RuleFor(x => x.LastName).NotEmpty().When(x => x.LastName is {}).WithMessage("Nazwisko nie może być puste."); ;
            RuleFor(d => d.Age).GreaterThan(0).LessThan(121).WithMessage("Wiek pomiędzy 1 a 120 lat");
            RuleFor(x => x.Gender).IsInEnum().NotEmpty().When(x => x.Gender is {})
                .WithMessage("Podana płeć nie istnieje.");
            RuleFor(x => x.DreamUrl).NotEmpty().When(x => x.DreamUrl is {}).WithMessage("Link do marzenia nie może być pusty.");
            RuleFor(x => x.Tags).NotEmpty().When(x => x.Tags is {}).WithMessage("Tagi nie mogą być puste.");
            RuleFor(x => x.DreamCategoryId).MustAsync(DreamCategoryExists).WithMessage(r=>$"Kategoria o Id=[{r.DreamCategoryId}] nie istnieje");
            _dbContext = dbContext;
        }

        private async Task<bool> DreamExists(long dreamId, CancellationToken cancellationToken)
            => await _dbContext.Dreams.AnyAsync(x => x.DreamId == dreamId, cancellationToken: cancellationToken);

        private async Task<bool> DreamCategoryExists(long? dreamCategoryId, CancellationToken cancellationToken) 
            => await _dbContext.DreamCategories.AnyAsync(x => x.DreamCategoryId == dreamCategoryId, cancellationToken: cancellationToken);
    }
}