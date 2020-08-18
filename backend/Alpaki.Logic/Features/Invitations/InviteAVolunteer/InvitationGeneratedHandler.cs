using System.Threading;
using System.Threading.Tasks;
using Alpaki.Logic.MailKit.Builders;
using Alpaki.Logic.Mails;
using Alpaki.Logic.Mails.Templates;
using MediatR;
using Microsoft.Extensions.Options;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InvitationGeneratedHandler : INotificationHandler<InvitationGenerated>
    {
        private readonly IMailService _mailService;
        private readonly MailKitOptions _options;

        public InvitationGeneratedHandler(IMailService mailService, IOptions<MailKitOptions> options)
        {
            _mailService = mailService;
            _options = options.Value;
        }
        public async Task Handle(InvitationGenerated notification, CancellationToken cancellationToken)
        {
            var message = MessageBuilder
                .Create()
                .WithReceiver(notification.Email)
                .WithSender(_options.Email)
                .WithSubject(MessageTemplates.Invitation.Subject)
                .WithBody(MessageTemplates.Invitation.Body, notification.UniqueCode)
                .Build();

            await _mailService.Send(message);
        }
    }
}