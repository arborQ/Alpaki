using System.Net;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Models;
using Alpaki.Logic.Handlers.AssignVolunteer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerDreamAssignController
    {
        private readonly IMediator _mediator;

        public VolunteerDreamAssignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task AssingVolunteerToDream(AssignVolunteerRequest assignVolunteerRequest) => await _mediator.Publish(assignVolunteerRequest);
    }
}
