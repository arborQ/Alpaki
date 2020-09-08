using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamStateEnumType : EnumerationGraphType<DreamStateEnum>
    {
    }

    public class DreamType : ObjectGraphType<Dream>
    {

        public DreamType()
        {
            Field(d => d.DreamId);
            Field(d => d.Age);
            Field(d => d.DisplayName);
            Field(d => d.Tags);
            Field(d => d.DreamComeTrueDate);
            Field(d => d.CityName);
            Field(d => d.Title);
            Field(d => d.DreamCategory.DreamCategoryId);
            Field(d => d.DreamCategory.CategoryName);
            Field("StepCount", d => d.RequiredSteps.Count());
            Field("FinishedStepCount", d => d.RequiredSteps.Count(s => s.StepState != StepStateEnum.Awaiting));
            Field(d => d.DreamImageId, type: typeof(IdGraphType));
            Field<ImageType>(nameof(Dream.DreamImage));
            Field<AssignedSponsorType>(nameof(Dream.Sponsors));
            Field<DreamCategoryType>(nameof(Dream.DreamCategory));
            Field<DreamStateEnumType>(nameof(Dream.DreamState));
            Field<ListGraphType<DeamStepType>>(nameof(Dream.RequiredSteps));
        }
    }

    public class ImageType : ObjectGraphType<Image>
    {
        public ImageType()
        {
            Field<IdGraphType>(nameof(Image.ImageData));
        }
    }
}
