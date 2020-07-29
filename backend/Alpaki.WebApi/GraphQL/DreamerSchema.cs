using Alpaki.CrossCutting.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamerSchema : Schema
    {
        public DreamerSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<AdminDreamerQuery>();
        }
    }
}
