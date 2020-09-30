using System.Linq;
using Alpaki.Logic.Extensions;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public abstract class PagedCollection<TType, TModel> : ObjectGraphType
        where TType: ObjectGraphType<TModel>
        where TModel: class
    {
        public PagedCollection()
        {
            Field<ListGraphType<TType>>("items", arguments: QueryArguments, resolve: context => FilterItemsPaged(context as ResolveFieldContext));
            Field<IntGraphType>("totalCount", arguments: QueryArguments, resolve: context => FilterItems(context as ResolveFieldContext).CountAsync());
        }

        public abstract QueryArguments QueryArguments { get; }

        protected abstract IQueryable<TModel> FilterItems(ResolveFieldContext context);

        protected IQueryable<TModel> FilterItemsPaged(ResolveFieldContext context, string argumentName = "page")
        {
            var page = new FilterField<int?, IntGraphType>(argumentName).Value(context);

            return FilterItems(context).Paged(page, 20);
        }
    }
}
