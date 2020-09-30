using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class MotoGraphQuery : ObjectGraphType
    {
        public MotoGraphQuery()
        {
            Field<BrandPagedCollectionType>("brands", resolve: context => new { });
        }
    }
}
