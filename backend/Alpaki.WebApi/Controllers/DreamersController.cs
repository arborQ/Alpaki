using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [VolunteerAccess]
        public async Task<UpdateDreamerResponse> UpdateDream([FromBody] UpdateDreamerRequest request)
            => await _mediator.Send(request);
    }
}
