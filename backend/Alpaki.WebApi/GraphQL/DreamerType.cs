using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class GenderEnumType : EnumerationGraphType<GenderEnum>
    {
        public GenderEnumType()
        {
        }
    }

    public class DreamerType : ObjectGraphType<Dreamer>
    {
        public DreamerType()
        {
            Field(d => d.Age);
            Field(d => d.DreamerId);
            Field(d => d.FirstName);
            Field(d => d.LastName);
            Field<GenderEnumType>(nameof(Dreamer.Gender));
            Field<ListGraphType<DreamType>>(nameof(Dreamer.Dreams));
        }
    }
}
