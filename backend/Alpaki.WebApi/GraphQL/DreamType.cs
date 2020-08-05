using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamStateEnumType : EnumerationGraphType<DreamStateEnum>
    {
    }


    public class GenderEnumType : EnumerationGraphType<GenderEnum>
    {

    }

    public class DreamType : ObjectGraphType<Dream>
    {

        public DreamType()
        {
            Field(d => d.DreamId);
            Field(d => d.Age);
            Field(d => d.FirstName);
            Field(d => d.LastName);
            Field(d => d.Tags);
            Field(d => d.DreamComeTrueDate);
            Field(d => d.DreamUrl);
            Field<GenderEnumType>(nameof(Dream.Gender));
            Field<DreamCategoryType>(nameof(Dream.DreamCategory));
            Field<DreamStateEnumType>(nameof(Dream.DreamState));
            Field<ListGraphType<DeamStepType>>(nameof(Dream.RequiredSteps));

        }
    }
}
