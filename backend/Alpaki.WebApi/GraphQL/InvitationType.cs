using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class InvitationType : ObjectGraphType<Invitation>
    {
        public class InvitationStateEnumType : EnumerationGraphType<InvitationStateEnum>
        {
        }
        public InvitationType()
        {
            Field(x => x.InvitationId);
            Field(x => x.Email);
            Field(x => x.Code);
            Field<InvitationStateEnumType>(nameof(Invitation.Status));
        }
    }
}