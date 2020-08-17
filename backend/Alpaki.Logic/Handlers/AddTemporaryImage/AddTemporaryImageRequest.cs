using MediatR;

namespace Alpaki.Logic.Handlers.AddTemporaryImage
{
    public class AddTemporaryImageRequest : IRequest<AddTemporaryImageResponse>
    {
        public byte[] ImageData { get; set; }
    }
}
