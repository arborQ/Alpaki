using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{

    public class DreamType : ObjectGraphType<Dream>
    {
        public class DreamStateEnumType : EnumerationGraphType<DreamStateEnum>
        {
        }

        public DreamType()
        {
            Field(d => d.Tags);
            Field(d => d.DreamComeTrueDate);
            Field<DreamerType>(nameof(Dream.Dreamer));
            Field<DreamCategoryType>(nameof(Dream.DreamCategory));
            Field<DreamStateEnumType>(nameof(Dream.DreamState));
            Field<ListGraphType<DeamStepType>>(nameof(Dream.RequiredSteps));

        }
    }
}
