using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.Logic.Handlers.GetDreams;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.UpdateDreamer;

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
        [VolunteerAccess]
        public Task<CreateDreamerResponse> CreateDream([FromBody] CreateDreamerRequest request) => _mediator.Send(request);

        [HttpPut]
        [CoordinatorAccess]
        public async Task<UpdateDreamerResponse> UpdateDream([FromBody] UpdateDreamerRequest request)
            => await _mediator.Send(request);

        [VolunteerAccess]
        [HttpGet]
        [ProducesResponseType(typeof(GetDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetDreamsResponse> GetDreams([FromQuery] GetDreamsRequest request) => _mediator.Send(request);

        [VolunteerAccess]
        [HttpGet("{dreamId}")]
        [ProducesResponseType(typeof(GetDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetDreamResponse> GetDreamDetails([FromRoute] long dreamId) => _mediator.Send(new GetDreamRequest { DreamId = dreamId });
    }
}
