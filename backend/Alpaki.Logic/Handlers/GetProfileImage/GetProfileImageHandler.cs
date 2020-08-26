using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetProfileImage
{
    public class GetProfileImageHandler : IRequestHandler<GetProfileImageRequest, GetProfileImageResponse>
    {
        private readonly IUserScopedDatabaseReadContext _userScopedDatabaseReadContext;

        public GetProfileImageHandler(IUserScopedDatabaseReadContext userScopedDatabaseReadContext)
        {
            _userScopedDatabaseReadContext = userScopedDatabaseReadContext;
        }

        public async Task<GetProfileImageResponse> Handle(GetProfileImageRequest request, CancellationToken cancellationToken)
        {
            var imageData = await _userScopedDatabaseReadContext.Images.SingleAsync(i => i.ImageId == request.ProfileImageId);

            return new GetProfileImageResponse { ImageData = imageData.ImageData };
        }
    }
}
