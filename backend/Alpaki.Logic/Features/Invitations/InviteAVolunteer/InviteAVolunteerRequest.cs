using MediatR;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerRequest : IRequest<InviteAVolunteerResponse>
    {
        public string Email { get; set; }
    }
}