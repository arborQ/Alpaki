using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateDreamer
{
    public class UpdateDreamerRequestValidator : AbstractValidator<UpdateDreamerRequest>
    {
        private readonly IDatabaseContext _dbContext;

        public UpdateDreamerRequestValidator(IDatabaseContext dbContext)
        {
            RuleFor(x => x.DreamId).NotNull();
            RuleFor(x => x.FirstName).NotEmpty().When(x => x.FirstName is {}).WithMessage("Imię nie może składać się wyłącznie z białych znaków.");
            RuleFor(x => x.LastName).NotEmpty().When(x => x.LastName is {}).WithMessage("Nazwisko nie może składać się wyłącznie z białych znaków."); ;
            RuleFor(d => d.Age).GreaterThan(0).LessThan(121).WithMessage("Wiek pomiędzy 1 a 120 lat");
            RuleFor(x => x.Gender).IsInEnum().NotEmpty().When(x => x.Gender is {})
                .WithMessage("Podana płeć nie istnieje.");
            RuleFor(x => x.DreamUrl).NotEmpty().When(x => x.DreamUrl is {});
            RuleFor(x => x.Tags).NotEmpty().When(x => x.Tags is {});
            RuleFor(x => x.DreamCategoryId).MustAsync(DreamCategoryExists).WithMessage(r=>$"Kategoria o Id=[{r.DreamCategoryId}] nie istnieje");
            _dbContext = dbContext;
        }

        private async Task<bool> DreamCategoryExists(long? dreamCategoryId, CancellationToken cancellationToken) 
            => await _dbContext.DreamCategories.AnyAsync(x => x.DreamCategoryId == dreamCategoryId, cancellationToken: cancellationToken);
    }
}