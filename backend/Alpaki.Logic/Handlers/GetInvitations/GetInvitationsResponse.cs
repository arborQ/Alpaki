using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetInvitations
{
    public class GetInvitationsResponse
    {
        public IReadOnlyCollection<InvitationListItem> Invitations { get; set; }

        public class InvitationListItem
        {
            public int InvitationId { get; set; }

            public string Email { get; set; }

            public string Code { get; set; }

            public InvitationStateEnum Status { get; set; }

            public DateTimeOffset CreatedAt { get; set; }

            internal static Expression<Func<Invitation, InvitationListItem>> InvitationToInvitationListItemMapper = user => new InvitationListItem
            {
                InvitationId = user.InvitationId,
                Email = user.Email,
                Code = user.Code,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
            };
        }
    }
}
