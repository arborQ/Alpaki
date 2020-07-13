using System.Threading.Tasks;
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
        public async Task AssingVolunteerToDream(AssignVolunteerRequest assignVolunteerRequest) => await _mediator.Send(assignVolunteerRequest);
    }
}
