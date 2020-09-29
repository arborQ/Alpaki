using MediatR;

namespace Alpaki.MotoLogic.Handlers.CreateBrand
{
    public class CreateBrandRequest : IRequest<CreateBrandResponse>
    {
        public string BrandName { get; set; }
    }
}
