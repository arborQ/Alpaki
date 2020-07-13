namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerResponse
    {
        public int InvitationId { get; }
        public string InvitationCode { get; }

        public InviteAVolunteerResponse(int invitationId, string invitationCode)
        {
            InvitationId = invitationId;
            InvitationCode = invitationCode;
        }
    }
}