using System.Threading;
using System.Threading.Tasks;
using Alpaki.Moto.Database;
using Alpaki.Moto.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.MotoLogic.Handlers.UpdateBrand
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandRequest, UpdateBrandResponse>
    {
        private readonly IMotoDatabaseContext _motoDatabaseContext;

        public UpdateBrandHandler(IMotoDatabaseContext motoDatabaseContext)
        {
            _motoDatabaseContext = motoDatabaseContext;
        }

        public async Task<UpdateBrandResponse> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = await _motoDatabaseContext
                .Brands
                .Include(b => b.BrandDomainEvents)
                .SingleAsync(b => b.BrandId == request.BrandId, cancellationToken);

            if (!brand.BrandName.Equals(request.BrandName))
            {
                brand.BrandName = request.BrandName;
                _motoDatabaseContext.BrandDomainEvents.Add(new BrandDomainEvent(CrossCutting.Enums.DomainEventType.Edit) { Brand = brand });

                await _motoDatabaseContext.SaveChangesAsync();
            }

            return new UpdateBrandResponse();
        }
    }
}
