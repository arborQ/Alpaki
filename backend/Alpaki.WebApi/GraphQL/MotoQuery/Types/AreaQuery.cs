using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Extensions;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL.MotoQuery.Types
{
    public abstract class AreaQuery<T> where T : class
    {
        private readonly ApplicationType _applicationType;
        protected readonly ICurrentUserService _currentUserService;

        public AreaQuery(ApplicationType applicationType, ICurrentUserService currentUserService)
        {
            _applicationType = applicationType;
            _currentUserService = currentUserService;
        }

        public abstract QueryArguments QueryArguments { get; }

        public IQueryable<T> FilterItems(ResolveFieldContext<object> context)
        {
            //if (!_currentUserService.ApplicationType.HasFlag(_applicationType))
            //{
            //    return DefaultQuery(context);
            //}

            return QueryFilterItems(context).Paged(0, 10);
        }

        protected virtual IQueryable<T> DefaultQuery(ResolveFieldContext<object> context)
        {
            return Enumerable.Empty<T>().AsQueryable();
        }

        protected abstract IQueryable<T> QueryFilterItems(ResolveFieldContext<object> context);
    }
}
