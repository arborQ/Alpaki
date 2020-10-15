using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Extensions;
using Alpaki.Moto.Database;
using Alpaki.Moto.Database.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class BrandsQuery : AreaQuery<Brand>
    {
        private readonly IMotoDatabaseContext _motoDatabaseContext;
        private readonly FilterField<string, StringGraphType> _searchArgument = new FilterField<string, StringGraphType>("search");
        private readonly FilterField<int?, IntGraphType> _pageArgument = new FilterField<int?, IntGraphType>("page");
        public override QueryArguments QueryArguments => new QueryArguments(
                    _searchArgument.GraphType,
                    _pageArgument.GraphType);



        public BrandsQuery(IMotoDatabaseContext motoDatabaseContext, ICurrentUserService currentUserService)
            :base(ApplicationType.Moto, currentUserService)
        {
            _motoDatabaseContext = motoDatabaseContext;
        }
        
        protected override IQueryable<Brand> QueryFilterItems(ResolveFieldContext context)
        {
            var page = _pageArgument.Value(context);
            var search = _searchArgument.Value(context);
            var contextxx = context.UserContext;
            return _motoDatabaseContext
                .Brands
                .Include(b => b.Models)
                .Where(b => string.IsNullOrEmpty(search) || b.BrandName.Contains(search) || b.Models.Any(m => m.ModelName.Contains(search)))
                .OrderBy(b => b.BrandName)
                .Paged(page)
                .AsNoTracking();
        }
    }
}
