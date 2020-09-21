using GraphQL;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.DreamQuery
{
    public class DreamerSchema : Schema
    {
        public DreamerSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<Query>();
        }
    }
}
