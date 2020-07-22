using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Logic.Features.Invitations.Repositories;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class FakeInvitationRepository : IInvitationRepository
    {
        private readonly List<Invitation> _invitations = new List<Invitation>();
        private int _idCounter = 0;

        public Invitation Get(int id)
        {
            var invitation = _invitations.First(x => x.InvitationId == id);
            return new Invitation
            {
                InvitationId = invitation.InvitationId, 
                Attempts = invitation.Attempts, 
                Code = invitation.Code, 
                Status = invitation.Status,
                CreatedAt = invitation.CreatedAt, 
                Email = invitation.Email
            };
        }

        public Task<Invitation> GetInvitationAsync(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(_invitations.FirstOrDefault(x=>x.Email.ToLower()== email.ToLower()));
        }

        public Task AddAsync(Invitation invitation, CancellationToken cancellationToken)
        {
            invitation.InvitationId = Next();
            _invitations.Add(invitation);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Invitation invitation, CancellationToken cancellationToken)
        {
            var dbInvitation = _invitations.Single(x => x.InvitationId == invitation.InvitationId);
            dbInvitation.Email = invitation.Email;
            dbInvitation.Attempts = invitation.Attempts;
            dbInvitation.Code = invitation.Code;
            dbInvitation.CreatedAt = invitation.CreatedAt;
            dbInvitation.Status = invitation.Status;
            return Task.CompletedTask;
        }

        private int Next() => ++_idCounter;
    }
}