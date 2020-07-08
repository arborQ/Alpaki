using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DreamersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DreamersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreateDreamerResponse> GetDreamer([FromBody] CreateDreamerRequest request) {

            var response = await _mediator.Send(request);

            return response;
        }
    }
}
