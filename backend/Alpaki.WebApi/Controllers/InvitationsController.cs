using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
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
        public async Task<InviteAVolunteerResponse> Post([FromBody]InviteAVolunteerRequest request) 
            => await _mediator.Send(request);
    }
}