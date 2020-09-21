using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class FilterField<T, GT> where GT : IGraphType
    {
        public T Value(ResolveFieldContext<object> context)
        {
            return context.GetArgument<T>(Name);
        }

        public QueryArgument<GT> GraphType => new QueryArgument<GT> { Name = Name };

        public string Name { get; private set; }

        public FilterField(string name)
        {
            Name = name;
        }
    }
}
