using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.DeleteUser;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Handlers.ChangeUserRole;
using Alpaki.Logic.Handlers.DeleteUser;
using Alpaki.Logic.Handlers.UpdateUserData;
using Alpaki.WebApi.Policies;
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

        [VolunteerAccess]
        [HttpPatch("me")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<UpdateUserDataResponse> UpdateUserData(UpdateUserDataRequest updateUserDataRequest) => _mediator.Send(updateUserDataRequest);

        [AdminAccess]
        [HttpPost("role")]
        public async Task<ChangeUserRoleResponse> ChangeUserRole(ChangeUserRoleRequest request) => await _mediator.Send(request);

        [AdminAccess]
        [HttpDelete]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<DeleteUserResponse> DeleteUser([FromQuery]DeleteUserRequest deleteUserRequest) => _mediator.Send(deleteUserRequest);
    }
}
