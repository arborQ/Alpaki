using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Logic.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetDreams.GetDreamsResponse;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamsHandler : IRequestHandler<GetDreamsRequest, GetDreamsResponse>
    {
        private readonly IUserScopedDatabaseReadContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public GetDreamsHandler(IUserScopedDatabaseReadContext databaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _currentUserService = currentUserService;
        }

        public async Task<GetDreamsResponse> Handle(GetDreamsRequest request, CancellationToken cancellationToken)
        {
            var dreamerQuery = _databaseContext.Dreams;

            if (request.Status.HasValue)
            {
                dreamerQuery = dreamerQuery.Where(d => d.DreamState == request.Status.Value);
            }

            if (request.AgeFrom.HasValue)
            {
                dreamerQuery = dreamerQuery.Where(d => d.Age >= request.AgeFrom.Value);
            }

            if (request.AgeTo.HasValue)
            {
                dreamerQuery = dreamerQuery.Where(d => d.Age <= request.AgeTo.Value);
            }

            if (request.Categories != null && request.Categories.Any())
            {
                dreamerQuery = dreamerQuery.Where(d => request.Categories.Contains(d.DreamCategoryId));
            }

            if (!string.IsNullOrEmpty(request.SearchName))
            {
                dreamerQuery = dreamerQuery.Where(d => d.DisplayName.Contains(request.SearchName));
            }

            dreamerQuery = dreamerQuery
                .OrderByProperty(request.OrderBy, request.Asc)
                .Paged(request.Page);
            
            var dreamList = await dreamerQuery.Select(DreamListItem.DreamToDreamListItemMapper).ToListAsync();

            return new GetDreamsResponse { Dreams = dreamList };
        }
    }
}
