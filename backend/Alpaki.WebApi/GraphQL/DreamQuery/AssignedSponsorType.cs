using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.DreamQuery
{
    public class AssignedSponsorType : ObjectGraphType<AssignedSponsor>
    {
        public AssignedSponsorType()
        {
            Field(s => s.SponsorId);
            Field<SponsorType>(nameof(AssignedSponsor.Sponsor));
        }
    }
}
