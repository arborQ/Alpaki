using MediatR;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class AssignVolunteerRequest : IRequest<AssignVolunteerResponse>
    {
        public long VolunteerId { get; set; }

        public long DreamId { get; set; }
    }
}
