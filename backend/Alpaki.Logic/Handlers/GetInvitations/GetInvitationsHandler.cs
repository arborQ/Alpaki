using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetInvitations.GetInvitationsResponse;

namespace Alpaki.Logic.Handlers.GetInvitations
{

    public class GetInvitationsHandler : IRequestHandler<GetInvitationsRequest, GetInvitationsResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public GetInvitationsHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<GetInvitationsResponse> Handle(GetInvitationsRequest request, CancellationToken cancellationToken)
        {

            var invitations = await _databaseContext
                .Invitations
                .AsNoTracking()
                .Where(x => x.Status == InvitationStateEnum.Pending)
                .Select(InvitationListItem.InvitationToInvitationListItemMapper)
                .ToListAsync(cancellationToken);

            return new GetInvitationsResponse { Invitations = invitations };
        }
    }
}
