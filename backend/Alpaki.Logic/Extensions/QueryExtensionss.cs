﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alpaki.Logic.Extensions
{
    public static class QueryExtensionss
    {
        public static IQueryable<TEntity> OrderByProperty<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                          bool asc)
        {
            string command = asc ? "OrderBy" : "OrderByDescending";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> source, int? pageNumber, int pageSize = 10)
        {
            if (!pageNumber.HasValue)
            {
                return source;
            }

            return source.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);
        }
    }
}
