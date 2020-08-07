using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
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
                .DreamCategories.Select(c => new Category { DreamCategoryId = c.DreamCategoryId, CategoryName = c.CategoryName })
                .ToListAsync();

            return new GetCategoriesResponse { Categories = categories };
        }
    }
}
