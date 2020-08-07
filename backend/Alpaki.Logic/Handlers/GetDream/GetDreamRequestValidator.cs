using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamRequestValidator : AbstractValidator<GetDreamRequest>
    {
        public GetDreamRequestValidator(IDatabaseContext databaseContext)
        {
            RuleFor(r => r.DreamId)
                .MustAsync((dreamId, cancelationToken) => databaseContext.Dreams.AnyAsync(d => d.DreamId == dreamId, cancelationToken))
                .WithMessage(d => $"Marzenie o Id=[{d.DreamId}] nie istnieje");
        }
    }
}
