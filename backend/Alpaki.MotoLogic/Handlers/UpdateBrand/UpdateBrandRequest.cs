using MediatR;

namespace Alpaki.MotoLogic.Handlers.UpdateBrand
{
    public class UpdateBrandRequest : IRequest<UpdateBrandResponse>
    {
        public long BrandId { get; set; }

        public string BrandName { get; set; }
    }
}
