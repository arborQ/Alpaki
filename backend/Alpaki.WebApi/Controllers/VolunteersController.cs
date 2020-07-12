using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using MediatR;
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
        public async Task<RegisterVolunteerResponse> Post([FromBody]RegisterVolunteer request) 
            => await _mediator.Send(request);
    }
}