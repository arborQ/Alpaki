using System.Net;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Handlers.AuthorizeUserPassword;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    public class AuthorizeService : Authorize.AuthorizeBase
    {
        private readonly IMediator _mediator;

        public AuthorizeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<AuthorizeResponse> ValidateUser(AuthorizeRequest request, ServerCallContext context)
        {
            var validateUser = await _mediator.Send(new AuthorizeUserPasswordRequest { Login = request.Login, Password = request.Password });

            return new AuthorizeResponse { Token = validateUser.Token };
        }
    }


    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public AuthorizeController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorizeUserPasswordResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<AuthorizeUserPasswordResponse> AuthorizeUser([FromBody] AuthorizeUserPasswordRequest authorizeUser) => _mediator.Send(authorizeUser);

        [HttpGet]
        public IActionResult CurrentToken()
        {
            return Ok(new
            {
                _currentUserService.ApplicationType,
                _currentUserService.CurrentUserId,
                _currentUserService.CurrentUserRole
            });
        }
    }
}
