using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetCategories.GetCategoriesResponse;

namespace Alpaki.Logic.Handlers.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public GetCategoriesHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<GetCategoriesResponse> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = await _databaseContext
                .DreamCategories
                .AsNoTracking()
                .Select(c => new Category { 
                    CategoryId = c.DreamCategoryId, 
                    CategoryName = c.CategoryName 
                })
                .ToListAsync(cancellationToken);

            return new GetCategoriesResponse { Categories = categories };
        }
    }
}
