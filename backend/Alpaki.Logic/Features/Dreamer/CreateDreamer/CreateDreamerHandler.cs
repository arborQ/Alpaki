using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using MediatR;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerHandler : IRequestHandler<CreateDreamerRequest, CreateDreamerResponse>
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public CreateDreamerHandler(IDatabaseContext databaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _currentUserService = currentUserService;
        }

        public async Task<CreateDreamerResponse> Handle(CreateDreamerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.CurrentUserId;
                var newDream = new Database.Models.Dream
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    DreamUrl = request.DreamUrl,
                    Gender = request.Gender,
                    DreamCategoryId = request.CategoryId,
                    Tags = request.Tags,
                    DreamState = DreamStateEnum.Created
                };

                await _databaseContext.Dreams.AddAsync(newDream);
                await _databaseContext.SaveChangesAsync();

                return new CreateDreamerResponse { DreamerId = newDream.DreamId };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
