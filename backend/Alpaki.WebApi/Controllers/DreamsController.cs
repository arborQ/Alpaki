﻿using Alpaki.Logic.Handlers.GetDreams;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AddDream;
using Alpaki.Logic.Handlers.UpdateDream;
using Microsoft.AspNetCore.Authorization;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DreamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DreamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [VolunteerAccess]
        [ProducesResponseType(typeof(AddDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<AddDreamResponse> CreateDream([FromBody] AddDreamRequest request) => _mediator.Send(request);

        [HttpPut]
        [CoordinatorAccess]
        [ProducesResponseType(typeof(UpdateDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<UpdateDreamResponse> UpdateDream([FromBody] UpdateDreamRequest request)
            => await _mediator.Send(request);

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(GetDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetDreamsResponse> GetDreams([FromQuery] GetDreamsRequest request) => _mediator.Send(request);

        [VolunteerAccess]
        [HttpGet("details")]
        [ProducesResponseType(typeof(GetDreamResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetDreamResponse> GetDreamDetails([FromQuery] GetDreamRequest request) => _mediator.Send(request);
    }
}
