using MediatR;

namespace Alpaki.Logic.Handlers.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public UpdateCategoryRequest()
        {
            DefaultSteps = new CategoryDefaultStep[0];
        }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public CategoryDefaultStep[] DefaultSteps { get; set; }

        public class CategoryDefaultStep
        {
            public long CategoryDefaultStepId { get; set; }

            public int Order { get; set; }

            public string StepName { get; set; }

            public bool IsSponsorRelated { get; set; }
        }
    }
}
