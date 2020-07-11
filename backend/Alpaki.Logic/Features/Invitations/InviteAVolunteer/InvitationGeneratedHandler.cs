using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InvitationGeneratedHandler : INotificationHandler<InvitationGenerated>
    {
        public Task Handle(InvitationGenerated notification, CancellationToken cancellationToken)
        {
            //TODO implement sending the mail
            return Task.CompletedTask;
        }
    }
}