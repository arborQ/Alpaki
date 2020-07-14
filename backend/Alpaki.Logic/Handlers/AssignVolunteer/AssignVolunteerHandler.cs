using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class AssignVolunteerHandler : IRequestHandler<AssignVolunteerRequest, AssignVolunteerResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public AssignVolunteerHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AssignVolunteerResponse> Handle(AssignVolunteerRequest request, CancellationToken cancellationToken)
        {
            var newAssign = new AssignedDreams() { VolunteerId = request.VolunteerId, DreamId = request.DreamId };

            await _databaseContext.AssignedDreams.AddAsync(newAssign);
            await _databaseContext.SaveChangesAsync();

            return new AssignVolunteerResponse();
        }
    }
}
