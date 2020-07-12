using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models.Invitations;
using MediatR;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerHandler : IRequestHandler<InviteAVolunteerRequest, InviteAVolunteerResponse>
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly InvitationUniqueCodesGenerator _generator;
        private readonly IMediator _mediator;

        public InviteAVolunteerHandler(IDatabaseContext databaseContext, InvitationUniqueCodesGenerator generator, IMediator mediator)
        {
            _databaseContext = databaseContext;
            _generator = generator;
            _mediator = mediator;
        }
        public async Task<InviteAVolunteerResponse> Handle(InviteAVolunteerRequest request, CancellationToken cancellationToken)
        {
            var existingInvitation = _databaseContext.Invitations.SingleOrDefault(x => x.Email.ToLower() == request.Email.ToLower() && x.Status == InvitationStateEnum.Pending);

            var invitation  = existingInvitation is null 
                ? await CreateNew(request.Email, cancellationToken)
                : await RefreshExisting(existingInvitation, cancellationToken);

            await _mediator.Publish(new InvitationGenerated(invitation.InvitationId, invitation.Email, invitation.Code), cancellationToken);

            return new InviteAVolunteerResponse(invitation.InvitationId);
        }

        private async Task<Invitation> CreateNew(string email, CancellationToken cancellationToken)
        {
            var code = _generator.Generate(4);
            var newInvitation = new Invitation
            {
                Email = email,
                Code = code,
                Timestamp = DateTimeOffset.UtcNow,
                Attempts = 0
            };

            await _databaseContext.Invitations.AddAsync(newInvitation, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return newInvitation;
        }

        private async Task<Invitation> RefreshExisting(Invitation invitation, CancellationToken cancellationToken)
        {
            var code = _generator.Generate(4);
            invitation.Code = code;
            invitation.Timestamp = DateTimeOffset.UtcNow;
            invitation.Attempts = 0;

            _databaseContext.Invitations.Update(invitation);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            return invitation;
        }
    }
}