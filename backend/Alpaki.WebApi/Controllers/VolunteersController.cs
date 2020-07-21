using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using Alpaki.Logic.Handlers.AssignVolunteer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VolunteersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VolunteersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterVolunteerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<RegisterVolunteerResponse> Post([FromBody]RegisterVolunteer request) 
            => await _mediator.Send(request);

        [HttpPost("assign")]
        [ProducesResponseType(typeof(UnassignVolunteerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task AssingVolunteerToDream(AssignVolunteerRequest assignVolunteeRequest) => await _mediator.Send(assignVolunteeRequest);

        [HttpDelete("assign")]
        [ProducesResponseType(typeof(UnassignVolunteerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task UnassingVolunteerToDream(UnassignVolunteerRequest assignVolunteeRequest) => await _mediator.Send(assignVolunteeRequest);
    }
}