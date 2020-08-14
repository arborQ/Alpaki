using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateUserData
{
    public class UpdateUserDataHandler : IRequestHandler<UpdateUserDataRequest, UpdateUserDataResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public UpdateUserDataHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UpdateUserDataResponse> Handle(UpdateUserDataRequest request, CancellationToken cancellationToken)
        {
            var user = await _databaseContext.Users.SingleAsync(u => u.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                user.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                user.LastName = request.LastName;
            }

            if (!string.IsNullOrEmpty(request.Brand))
            {
                user.Brand = request.Brand;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.ProfileImageId.HasValue)
            {
                user.ProfileImageId = request.ProfileImageId.Value;
            }

            await _databaseContext.SaveChangesAsync();

            return new UpdateUserDataResponse();
        }
    }
}
