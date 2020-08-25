using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AddSponsor;
using Alpaki.Logic.Handlers.DeleteSponsor;
using Alpaki.Logic.Handlers.GetSponsors;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SponsorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SponsorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [VolunteerAccess]
        [ProducesResponseType(typeof(GetSponsorsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<GetSponsorsResponse> GetSponsors([FromQuery] GetSponsorsRequest request) => _mediator.Send(request);

        [HttpPost]
        [CoordinatorAccess]
        [ProducesResponseType(typeof(AddSponsorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<AddSponsorResponse> AddSponsor([FromBody] AddSponsorRequest request) => _mediator.Send(request);

        [HttpDelete]
        [CoordinatorAccess]
        [ProducesResponseType(typeof(DeleteSponsorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<DeleteSponsorResponse> DeleteSponsor([FromQuery]DeleteSponsorRequest request) => _mediator.Send(request);
    }
}
