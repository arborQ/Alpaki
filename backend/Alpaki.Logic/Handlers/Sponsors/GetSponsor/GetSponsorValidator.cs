using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsor
{
    public class GetSponsorValidator : AbstractValidator<GetSponsorRequest>
    {
        private readonly IUserScopedDatabaseReadContext _dbContext;

        public GetSponsorValidator(IUserScopedDatabaseReadContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.SponsorId).MustAsync(Exits)
                .WithMessage(x => $"Sponsor od id [{nameof(x.SponsorId)}={x.SponsorId}] nie istnieje.");
        }

        public async Task<bool> Exits(long sponsorId, CancellationToken cancellationToken) 
            => await _dbContext.Sponsors.AsNoTracking().AnyAsync(x => x.SponsorId == sponsorId, cancellationToken: cancellationToken);
    }
}