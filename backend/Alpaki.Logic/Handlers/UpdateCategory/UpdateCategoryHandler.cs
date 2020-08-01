

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public UpdateCategoryHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _databaseContext.DreamCategories.Include(c => c.DefaultSteps).SingleAsync(c => c.DreamCategoryId == request.CategoryId);

            if (request.CategoryName != null)
            {
                category.CategoryName = request.CategoryName;
            }

            if (request.DefaultSteps.Any())
            {
                var toRemove = category.DefaultSteps.Where(c => request.DefaultSteps.All(r => r.CategoryDefaultStepId != c.DreamCategoryDefaultStepId)).ToList();

                _databaseContext.DreamCategoryDefaultSteps.RemoveRange(toRemove);

                foreach(var stepToRemove in toRemove)
                {
                    category.DefaultSteps.Remove(stepToRemove);
                }

                foreach (var dbCategory in category.DefaultSteps)
                {
                    var requestCategory = request.DefaultSteps.FirstOrDefault(c => c.CategoryDefaultStepId == dbCategory.DreamCategoryDefaultStepId);

                    if (requestCategory != null)
                    {
                        dbCategory.IsSponsorRelated = requestCategory.IsSponsorRelated;
                        dbCategory.StepDescription = requestCategory.StepName;
                    }
                }

                foreach (var requestCategory in request.DefaultSteps.Where(c => c.CategoryDefaultStepId == default))
                {
                    category.DefaultSteps.Add(new DreamCategoryDefaultStep { IsSponsorRelated = requestCategory.IsSponsorRelated, StepDescription = requestCategory.StepName });
                }
            }

            await _databaseContext.SaveChangesAsync();

            return new UpdateCategoryResponse { };
        }
    }
}
