using System.Threading;
using System.Threading.Tasks;
using Alpaki.Moto.Database;
using Alpaki.Moto.Database.Models;
using MediatR;

namespace Alpaki.MotoLogic.Handlers.CreateBrand
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandRequest, CreateBrandResponse>
    {
        private readonly IMotoDatabaseContext _motoDatabaseContext;

        public CreateBrandHandler(IMotoDatabaseContext motoDatabaseContext)
        {
            _motoDatabaseContext = motoDatabaseContext;
        }

        public async Task<CreateBrandResponse> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var newBrand = new Brand(request.BrandName);

            await _motoDatabaseContext.Brands.AddAsync(newBrand);
            await _motoDatabaseContext.SaveChangesAsync();

            return new CreateBrandResponse { BrandId = newBrand.BrandId };
        }
    }
}
