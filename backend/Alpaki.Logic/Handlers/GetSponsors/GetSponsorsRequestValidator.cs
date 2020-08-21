using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetSponsors
{
    public class GetSponsorsRequestValidator: AbstractValidator<GetSponsorsRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public GetSponsorsRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(r => r.DreamId).MustAsync(DreamExists).When(d => d.DreamId.HasValue).WithMessage(d => $"Marzenie od Id=[{d.DreamId}] nie istnieje.");
        }

        private Task<bool> DreamExists(long? dreamId, CancellationToken cancellationToken)
        {
            return _databaseContext.Dreams.AnyAsync(d => d.DreamId == dreamId, cancellationToken: cancellationToken);
        }
    }
}
