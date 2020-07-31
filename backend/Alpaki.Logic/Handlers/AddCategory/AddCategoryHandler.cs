using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.AddCategory
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, AddCategoryResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public AddCategoryHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AddCategoryResponse> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var newCategory = new DreamCategory
                {
                    CategoryName = request.CategoryName,
                    DefaultSteps = request.DefaultSteps.Select(ds => new DreamCategoryDefaultStep
                    {
                        IsSponsorRelated = ds.IsSponsorRelated,
                        StepDescription = ds.StepName
                    }).ToList()
                };

                await _databaseContext.DreamCategories.AddAsync(newCategory);
                await _databaseContext.SaveChangesAsync();

                return new AddCategoryResponse { DreamCategoryId = newCategory.DreamCategoryId };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
