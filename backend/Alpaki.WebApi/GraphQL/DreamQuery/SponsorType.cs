using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.DreamQuery
{
    public class CooperationTypeEnumType : EnumerationGraphType<SponsorTypeEnum>
    {
    }

    public class SponsorType : ObjectGraphType<Sponsor>
    {
        public SponsorType()
        {
            Field(s => s.SponsorId);
            Field(s => s.Email);
            Field(s => s.Brand);
            Field(s => s.ContactPersonName);
            Field(s => s.CooperationType, type: typeof(CooperationTypeEnumType));
            Field(s => s.DisplayName);
        }
    }
}
