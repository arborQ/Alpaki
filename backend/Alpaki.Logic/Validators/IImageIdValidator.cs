using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alpaki.Logic.Validators
{
    public interface IImageIdValidator
    {
        Task<bool> ImageIdIsAvailable(Guid?
            imageId, CancellationToken cancellationToken = default);
    }
}