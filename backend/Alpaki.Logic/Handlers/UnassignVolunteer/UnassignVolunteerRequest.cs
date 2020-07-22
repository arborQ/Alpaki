using MediatR;

namespace Alpaki.Logic.Handlers.AssignVolunteer
{
    public class UnassignVolunteerRequest : IRequest<UnassignVolunteerResponse>
    {
        public long VolunteerId { get; set; }

        public long DreamId { get; set; }
    }
}
