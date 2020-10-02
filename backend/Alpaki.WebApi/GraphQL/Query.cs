using Alpaki.WebApi.GraphQL.DreamQuery;
using Alpaki.WebApi.GraphQL.MotoQuery.Types;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Name = "Query";
            Field<AdminDreamerQuery>("dreams", resolve: context => new { });
            Field<MotoGraphQuery>("moto", resolve: context => new { });
        }
    }
}
