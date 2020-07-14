using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models.Invitations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerHandler : IRequestHandler<InviteAVolunteerRequest, InviteAVolunteerResponse>
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IInvitationCodesGenerator _generator;
        private readonly IMediator _mediator;
        private readonly ISystemClock _clock;

        public InviteAVolunteerHandler(IDatabaseContext databaseContext, IInvitationCodesGenerator generator, IMediator mediator, ISystemClock clock)
        {
            _databaseContext = databaseContext;
            _generator = generator;
            _mediator = mediator;
            _clock = clock;
        }
        public async Task<InviteAVolunteerResponse> Handle(InviteAVolunteerRequest request, CancellationToken cancellationToken)
        {
            if(await _databaseContext.Users.AnyAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()), cancellationToken))
                throw new Exceptions.VolunteerAlreadyExistsException();

            var existingInvitation = await _databaseContext.Invitations.SingleOrDefaultAsync(
                x => x.Email.ToLower() == request.Email.ToLower() 
                     && x.Status == InvitationStateEnum.Pending, cancellationToken);

            var invitation  = existingInvitation is null 
                ? await CreateNew(request.Email, cancellationToken)
                : await RefreshExisting(existingInvitation, cancellationToken);

            await _mediator.Publish(new InvitationGenerated(invitation.InvitationId, invitation.Email, invitation.Code), cancellationToken);

            return new InviteAVolunteerResponse(invitation.InvitationId, invitation.Code);
        }

        private async Task<Invitation> CreateNew(string email, CancellationToken cancellationToken)
        {
            var code = _generator.Generate(4);
            var newInvitation = new Invitation
            {
                Email = email,
                Code = code,
                CreatedAt = _clock.UtcNow,
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
            invitation.CreatedAt = _clock.UtcNow;
            invitation.Attempts = 0;

            _databaseContext.Invitations.Update(invitation);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            return invitation;
        }
    }
}