using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Features.Invitations.Repositories;
using MediatR;
using Microsoft.Extensions.Internal;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InviteAVolunteerHandler : IRequestHandler<InviteAVolunteerRequest, InviteAVolunteerResponse>
    {
        private readonly IInvitationCodesGenerator _generator;
        private readonly IMediator _mediator;
        private readonly ISystemClock _clock;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IVolunteerRepository _volunteerRepository;

        public InviteAVolunteerHandler(
            IInvitationCodesGenerator generator, 
            IMediator mediator, 
            ISystemClock clock,
            IInvitationRepository invitationRepository,
            IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
            _generator = generator;
            _mediator = mediator;
            _clock = clock;
            _invitationRepository = invitationRepository;
        }
        public async Task<InviteAVolunteerResponse> Handle(InviteAVolunteerRequest request, CancellationToken cancellationToken)
        {
            if(await _volunteerRepository.ExitsAsync(request.Email, cancellationToken))
                throw new Exceptions.VolunteerAlreadyExistsException();

            var existingInvitation = await _invitationRepository.GetInvitationAsync(request.Email, cancellationToken);

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

            await _invitationRepository.AddAsync(newInvitation, cancellationToken);

            return newInvitation;
        }

        private async Task<Invitation> RefreshExisting(Invitation invitation, CancellationToken cancellationToken)
        {
            var code = _generator.Generate(4);
            invitation.Code = code;
            invitation.CreatedAt = _clock.UtcNow;
            invitation.Attempts = 0;

            await _invitationRepository.UpdateAsync(invitation, cancellationToken);

            return invitation;
        }
    }
}