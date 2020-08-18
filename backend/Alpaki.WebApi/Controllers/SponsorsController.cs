using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.Sponsors.AddSponsor;
using Alpaki.Logic.Handlers.Sponsors.GetSponsors;
using Alpaki.Logic.Handlers.Sponsors.RemoveSponsor;
using Alpaki.Logic.Handlers.Sponsors.UpdateSponsor;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SponsorsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [VolunteerAccess]
        [ProducesResponseType(typeof(AddSponsorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AddSponsorResponse>> Post(AddSponsorRequest request)
            => Ok(await _mediator.Send(request));
        
        [HttpPut]
        [VolunteerAccess]
        [ProducesResponseType(typeof(AddSponsorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AddSponsorResponse>> Put(UpdateSponsorRequest request)
            => Ok(await _mediator.Send(request));
        
        [HttpDelete("{sponsorId}")]
        [VolunteerAccess]
        [ProducesResponseType(typeof(AddSponsorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AddSponsorResponse>> Delete(long sponsorId)
            => Ok(await _mediator.Send(new RemoveSponsorRequest{Id = sponsorId}));

        [HttpGet]
        [VolunteerAccess]
        [ProducesResponseType(typeof(GetSponsorsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetSponsorsResponse>> GetAll([FromQuery]GetSponsorsRequest request)
            => Ok(await _mediator.Send(request));
    }
}