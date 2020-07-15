using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Services
{
    public class UserAuthorizeHandler : IRequestHandler<UserAuthorizeRequest, UserAuthorizeResponse>
    {
        private readonly IValidator<UserAuthorizeRequest> _validator;
        private readonly IDatabaseContext _databaseContext;

        public UserAuthorizeHandler(IValidator<UserAuthorizeRequest> validator, IDatabaseContext databaseContext)
        {
            _validator = validator;
            _databaseContext = databaseContext;
        }

        public async Task<UserAuthorizeResponse> Handle(UserAuthorizeRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception("invalid request");
            }

            var user = await _databaseContext.Users.Where(u => u.Email == request.Login).FirstOrDefaultAsync(cancellationToken);

            return new UserAuthorizeResponse();
        }
    }
}
