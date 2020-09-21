using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.DreamQuery
{
    public class DreamCategoryType : ObjectGraphType<DreamCategory>
    {
        public DreamCategoryType()
        {
            Field(c => c.DreamCategoryId);
            Field(c => c.CategoryName);
            Field("dreamCount", c => c.Dreams.Count);
            Field<ListGraphType<DefaultStepType>>(nameof(DreamCategory.DefaultSteps));
        }
    }
}
