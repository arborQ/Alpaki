using MediatR;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class AddCategoryRequest : IRequest<AddCategoryResponse>
    {
        public string CategoryName { get; set; }
    }
}
