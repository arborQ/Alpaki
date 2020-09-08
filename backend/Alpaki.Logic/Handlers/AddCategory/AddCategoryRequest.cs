using MediatR;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class AddCategoryRequest : IRequest<AddCategoryResponse>
    {
        public AddCategoryRequest()
        {
            DefaultSteps = new AddCategoryDefaultStep[0];
        }

        public string CategoryName { get; set; }

        public AddCategoryDefaultStep[] DefaultSteps { get; set; }

        public class AddCategoryDefaultStep
        {
            public int Order { get; set; }

            public string StepDescription { get; set; }

            public bool IsSponsorRelated { get; set; }
        }
    }
}
