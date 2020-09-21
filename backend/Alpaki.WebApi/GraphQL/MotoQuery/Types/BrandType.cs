using Alpaki.Moto.Database.Models;
using GraphQL.Types;
using Microsoft.VisualBasic.CompilerServices;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class BrandType : ObjectGraphType<Brand>
    {
        public BrandType()
        {
            Field(d => d.BrandId);
            Field(d => d.BrandName);
            Field(d => d.IsActive);
            Field("modelCount", d => d.Models.Count);
            Field<ListGraphType<ModelType>>(nameof(Brand.Models));
        }
    }
}
