using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.AddTemporaryImage
{
    public class AddTemporaryImageHandler : IRequestHandler<AddTemporaryImageRequest, AddTemporaryImageResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public AddTemporaryImageHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AddTemporaryImageResponse> Handle(AddTemporaryImageRequest request, CancellationToken cancellationToken)
        {
            var newImage = new Image { Created = DateTime.Now, ImageData = request.ImageData };
            await _databaseContext.Images.AddAsync(newImage, cancellationToken);
            await _databaseContext.SaveChangesAsync();

            return new AddTemporaryImageResponse { ImageId = newImage.ImageId };
        }
    }
}
