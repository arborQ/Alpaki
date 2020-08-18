using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Validators
{
    public class ImageIdValidator: IImageIdValidator
    {
        private readonly IDatabaseContext _databaseContext;

        public ImageIdValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<bool> ImageIdIsAvailable(Guid? imageId, CancellationToken cancellationToken = default)
        {
            return _databaseContext.Users
                 .Select(u => u.ProfileImageId)
                 .Union(_databaseContext.Dreams.Select(d => d.DreamImageId)).AllAsync(id => id != imageId, cancellationToken);
        }
    }
}