using MediatR;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InvitationGenerated : INotification
    {
        public int InvitationId { get; }
        public string Email { get; }
        public string UniqueCode { get; }

        public InvitationGenerated(int invitationId, string email, string uniqueCode)
        {
            InvitationId = invitationId;
            Email = email;
            UniqueCode = uniqueCode;
        }
    }
}