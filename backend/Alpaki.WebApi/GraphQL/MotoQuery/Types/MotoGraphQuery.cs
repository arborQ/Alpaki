using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class MotoGraphQuery : ObjectGraphType
    {
        public MotoGraphQuery(BrandsQuery brandsQuery)
        {
            Field<ListGraphType<BrandType>>("brands", arguments: brandsQuery.QueryArguments, resolve: context => brandsQuery.FilterItems(context));
            Field<PagedCollection<BrandType>>("paged", arguments: brandsQuery.QueryArguments, resolve: context => new { });
        }
    }

    public class PagedCollection<TGraphType, TModel> : ObjectGraphType
        where TGraphType : IGraphType
        where TModel: class
    {
        private readonly AreaQuery<TModel> _queryService;

        public PagedCollection(AreaQuery<TModel> queryService, ResolveFieldContext<object> context)
        {
            _queryService = queryService;

            Items = _queryService.FilterItems(context)
        }

        public ListGraphType<TGraphType> Items { get; }

        public IntGraphType TotalCount { get; }
    }
}
