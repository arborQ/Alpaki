using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class MotoGraphQuery : ObjectGraphType
    {
        public MotoGraphQuery(BrandsQuery brandsQuery)
        {
            Field<ListGraphType<BrandType>>("brands", arguments: brandsQuery.QueryArguments, resolve: context => brandsQuery.FilterItems(context));
        }
    }
}
