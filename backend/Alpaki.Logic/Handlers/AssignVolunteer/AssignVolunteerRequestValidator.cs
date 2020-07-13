using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class AssignVolunteerRequestValidator : AbstractValidator<AssignVolunteerRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public AssignVolunteerRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(r => r).MustAsync(async (model, cancellationToken) =>
            {
                return !(await _databaseContext.AssignedDreams.AnyAsync(ad => ad.DreamId == model.DreamId && ad.VolunteerId == model.VolunteerId));
            }).WithMessage("Wolontariusz jest już przypisany do marzenia.");

            RuleFor(r => r.DreamId).MustAsync(DreamExists).WithMessage("Marzenie nie istnieje.");
            RuleFor(r => r.VolunteerId).MustAsync(VolunteerExists).WithMessage("Wolontariusz nie istnieje.");
        }

        private Task<bool> DreamExists(long dreamId, CancellationToken cancellationToken)
        {
            return _databaseContext.Dreams.AnyAsync(d => d.DreamId == dreamId, cancellationToken: cancellationToken);
        }

        private Task<bool> VolunteerExists(long volunteerId, CancellationToken cancellationToken)
        {
            return _databaseContext.Users.AnyAsync(d => d.UserId == volunteerId, cancellationToken: cancellationToken);
        }
    }
}
