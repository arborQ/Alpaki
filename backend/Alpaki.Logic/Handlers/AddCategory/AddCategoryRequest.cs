using MediatR;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class AddCategoryRequest : IRequest<AddCategoryResponse>
    {
        public AddCategoryRequest()
        {
            DefaultSteps = new CategoryDefaultStep[0];
        }

        public string CategoryName { get; set; }

        public CategoryDefaultStep[] DefaultSteps { get; set; }

        public class CategoryDefaultStep
        {
            public int Order { get; set; }

            public string StepDescription { get; set; }

            public bool IsSponsorRelated { get; set; }
        }
    }
}
