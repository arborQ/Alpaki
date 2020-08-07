using System.Collections.Generic;

namespace Alpaki.Logic.Handlers.GetCategories
{
    public class GetCategoriesResponse
    {
        public GetCategoriesResponse()
        {
            Categories = new Category[0];
        }

        public IReadOnlyCollection<Category> Categories { get; set; }

        public class Category
        {
            public long DreamCategoryId { get; set; }

            public string CategoryName { get; set; }
        }
    }
}
