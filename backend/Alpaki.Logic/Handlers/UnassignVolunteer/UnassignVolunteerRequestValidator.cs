using Alpaki.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class UnassignVolunteerRequestValidator : AbstractValidator<UnassignVolunteerRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public UnassignVolunteerRequestValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            RuleFor(r => r).MustAsync(async (model, cancellationToken) =>
            {
                return await _databaseContext.AssignedDreams.AnyAsync(ad => ad.DreamId == model.DreamId && ad.VolunteerId == model.VolunteerId);
            }).WithMessage("Wolontariusz nie jest przypisany do marzenia.");
        }
    }
}
