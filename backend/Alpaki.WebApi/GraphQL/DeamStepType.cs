using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DeamStepType : ObjectGraphType<DreamStep>
    {
        public class StepStateEnumType : EnumerationGraphType<StepStateEnum>
        {
        }

        public DeamStepType()
        {
            Field(s => s.DreamStepId);
            Field(s => s.StepDescription);
            Field<StepStateEnumType>(nameof(DreamStep.StepState));
        }
    }
}
