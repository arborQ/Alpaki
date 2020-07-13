using MediatR;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class AssignVolunteerRequest : INotification
    {
        public long VolunteerId { get; set; }

        public long DreamId { get; set; }
    }
}
