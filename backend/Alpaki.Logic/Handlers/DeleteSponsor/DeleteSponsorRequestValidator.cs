using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.DeleteSponsor
{
    public class DeleteSponsorRequestValidator : AbstractValidator<DeleteSponsorRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public DeleteSponsorRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public DeleteSponsorRequestValidator()
        {
            RuleFor(r => r.SponsorId).MustAsync(SponsorExists).WithMessage(s => $"Sponsor z Id=[{s.SponsorId}] nie istnieje");
        }

        private Task<bool> SponsorExists(long sponsorId, CancellationToken cancellationToken)
        {
            return _databaseContext.Sponsors.AnyAsync(s => s.SponsorId == sponsorId, cancellationToken: cancellationToken);
        }
    }
}
