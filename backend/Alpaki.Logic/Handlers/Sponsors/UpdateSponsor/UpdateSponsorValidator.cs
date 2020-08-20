using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.UpdateSponsor
{
    public class UpdateSponsorValidator : AbstractValidator<UpdateSponsorRequest>
    {
        private readonly IUserScopedDatabaseReadContext _dbContext;

        public UpdateSponsorValidator(IUserScopedDatabaseReadContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.Name).NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Nazwa sponsora nie może być pusta.");
            RuleFor(x => x.Id).MustAsync(Exits).WithMessage(x=> $"Sponsor od podanym id [SponsorId={x.Id}] nie istnieje.");;
        }

        private async Task<bool> Exits(long id, CancellationToken cancellationToken) 
            => await _dbContext.Sponsors.AnyAsync(x => x.SponsorId == id, cancellationToken: cancellationToken);
    }
}