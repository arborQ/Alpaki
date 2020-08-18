using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.RemoveSponsor
{
    public class RemoveSponsorValidator : AbstractValidator<RemoveSponsorRequest>
    {
        private readonly IUserScopedDatabaseReadContext _dbContext;

        public RemoveSponsorValidator(IUserScopedDatabaseReadContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.Id).MustAsync(Exits).WithMessage(x=> $"Sponsor od podanym id [Id={x.Id}] nie istnieje.");
        }

        private async Task<bool> Exits(long id, CancellationToken cancellationToken) 
            => await _dbContext.Sponsors.AnyAsync(x => x.SponsorId == id, cancellationToken: cancellationToken);
    }
}