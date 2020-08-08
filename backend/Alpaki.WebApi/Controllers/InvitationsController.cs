using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Logic.Handlers.GetInvitations;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [VolunteerAccess]
        [ProducesResponseType(typeof(InviteAVolunteerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<InviteAVolunteerResponse> SendInvitation([FromBody]InviteAVolunteerRequest request) => _mediator.Send(request);


        [HttpGet]
        [CoordinatorAccess]
        [ProducesResponseType(typeof(GetInvitationsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetInvitationsResponse> GetPendingInvitations([FromQuery]GetInvitationsRequest request) => _mediator.Send(request);
    }
}