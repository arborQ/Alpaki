using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class UnassignVolunteerHandler : IRequestHandler<UnassignVolunteerRequest, UnassignVolunteerResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public UnassignVolunteerHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UnassignVolunteerResponse> Handle(UnassignVolunteerRequest request, CancellationToken cancellationToken)
        {
            var existingAssign = await _databaseContext.AssignedDreams.SingleAsync(ad => ad.DreamId == request.DreamId && ad.VolunteerId == request.VolunteerId);

            _databaseContext.AssignedDreams.Remove(existingAssign);
            await _databaseContext.SaveChangesAsync();

            return new UnassignVolunteerResponse();
        }
    }
}
