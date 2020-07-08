using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamCategoryType: ObjectGraphType<DreamCategory>
    {
        public DreamCategoryType()
        {
            Field(c => c.DreamCategoryId);
            Field(c => c.CategoryName);
        }
    }
}
