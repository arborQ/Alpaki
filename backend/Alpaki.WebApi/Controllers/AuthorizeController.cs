using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AuthorizeUserPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorizeUserPasswordResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<AuthorizeUserPasswordResponse> AssingVolunteerToDream([FromBody] AuthorizeUserPasswordRequest authorizeUser) => _mediator.Send(authorizeUser);
    }
}
