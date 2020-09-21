using Alpaki.Moto.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class ModelType : ObjectGraphType<Model>
    {
        public ModelType()
        {
            Field(m => m.ModelId);
            Field(m => m.ModelName);
        }
    }
}
