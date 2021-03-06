﻿using System;
using System.Linq;
using System.Security.Claims;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Moto.Database;
using Alpaki.Moto.Database.Models;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public class BrandPagedCollectionType : PagedCollection<BrandType, Brand>
    {
        public BrandPagedCollectionType(IServiceProvider  serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private readonly FilterField<string, StringGraphType> _searchArgument = new FilterField<string, StringGraphType>("search");
        private readonly FilterField<int?, IntGraphType> _pageArgument = new FilterField<int?, IntGraphType>("page");
        private readonly FilterField<int?, IntGraphType> _brandIdArgument = new FilterField<int?, IntGraphType>("brandId");

        private readonly IServiceProvider _serviceProvider;

        public override QueryArguments QueryArguments => new QueryArguments(
                    _searchArgument.GraphType,
                    _pageArgument.GraphType, 
                    _brandIdArgument.GraphType);

        protected override IQueryable<Brand> FilterItems(ResolveFieldContext context)
        {
            var search = _searchArgument.Value(context);
            var brandId = _brandIdArgument.Value(context);

            return (_serviceProvider.GetService(typeof(IMotoDatabaseContext)) as IMotoDatabaseContext)
                .Brands
                .Include(b => b.Models)
                .Where(b => !brandId.HasValue || b.BrandId == brandId.Value)
                .Where(b => string.IsNullOrEmpty(search) || b.BrandName.Contains(search) || b.Models.Any(m => m.ModelName.Contains(search)))
                .OrderBy(b => b.BrandName)
                .AsNoTracking();
        }
    }
}
