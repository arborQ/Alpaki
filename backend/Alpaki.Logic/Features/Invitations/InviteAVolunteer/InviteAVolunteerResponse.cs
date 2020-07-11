namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerResponse
    {
        public int InvitationId { get; }

        public InviteAVolunteerResponse(int invitationId)
        {
            InvitationId = invitationId;
        }
    }
}