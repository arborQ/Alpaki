using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DefaultStepType : ObjectGraphType<DreamCategoryDefaultStep>
    {
        public DefaultStepType()
        {
            Field(c => c.DreamCategoryDefaultStepId);
            Field(c => c.StepDescription);
            Field(c => c.IsSponsorRelated);
        }
    }
}
