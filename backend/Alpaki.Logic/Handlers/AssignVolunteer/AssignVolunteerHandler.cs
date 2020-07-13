using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class AssignVolunteerHandler : INotificationHandler<AssignVolunteerRequest>
    {
        private readonly IDatabaseContext _databaseContext;

        public AssignVolunteerHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Handle(AssignVolunteerRequest notification, CancellationToken cancellationToken)
        {
            var newAssign = new AssignedDreams() { VolunteerId = notification.VolunteerId, DreamId = notification.DreamId };

            await _databaseContext.AssignedDreams.AddAsync(newAssign);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
