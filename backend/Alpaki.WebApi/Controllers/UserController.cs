using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.UpdateUserData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<UpdateUserDataResponse> UpdateUserData(UpdateUserDataRequest updateUserDataRequest) => _mediator.Send(updateUserDataRequest);
    }
}
