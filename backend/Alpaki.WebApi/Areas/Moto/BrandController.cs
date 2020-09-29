using System.Net;
using System.Threading.Tasks;
using Alpaki.MotoLogic.Handlers.CreateBrand;
using Alpaki.MotoLogic.Handlers.UpdateBrand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Areas.Moto
{
    [Area("Moto")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateBrandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<CreateBrandResponse> CreateBrand([FromBody] CreateBrandRequest request) => _mediator.Send(request);

        [HttpPut]
        [ProducesResponseType(typeof(CreateBrandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<UpdateBrandResponse> UpdateBrand([FromBody] UpdateBrandRequest request) => _mediator.Send(request);
    }
}
