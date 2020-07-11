using System;
using System.Threading;
using System.Threading.Tasks;
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
            var code = _generator.Generate(4);
            var invitation = new Invitation 
            {
                Email = request.Email, 
                Code = code,
                Timestamp = DateTimeOffset.UtcNow
            };

            await _databaseContext.Invitations.AddAsync(invitation, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new InvitationGenerated(invitation.InvitationId, invitation.Email, invitation.Code), cancellationToken);

            return new InviteAVolunteerResponse(invitation.InvitationId);
        }
    }
}