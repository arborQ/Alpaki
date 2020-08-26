using System;
using MediatR;

namespace Alpaki.Logic.Handlers.GetProfileImage
{
    public class GetProfileImageRequest : IRequest<GetProfileImageResponse>
    {
        public Guid ProfileImageId { get; set; }
    }
}
