using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.DeleteUser;
using Alpaki.Logic.Handlers.ChangeUserRole;
using Alpaki.Logic.Handlers.UpdateUserData;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Alpaki.Logic.Handlers.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Alpaki.Logic.Handlers.GetUser;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Handlers.RegisterNewUser;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<GetUserResponse> GetMyUserData() => _mediator.Send(new GetUserRequest { UserId = _currentUserService.CurrentUserId });

        [VolunteerAccess]
        [HttpPatch]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<UpdateUserDataResponse> UpdateUserData([FromBody] UpdateUserDataRequest updateUserDataRequest) => _mediator.Send(updateUserDataRequest);

        [VolunteerAccess]
        [HttpGet]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetUsersResponse> GetUsers([FromQuery] GetUsersRequest getUsersRequest) => _mediator.Send(getUsersRequest);

        [Authorize]
        [HttpGet("details")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<GetUserResponse> GetUser([FromQuery] GetUserRequest request) => _mediator.Send(request);

        [AdminAccess]
        [HttpPatch("role")]
        [ProducesResponseType(typeof(ChangeUserRoleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<ChangeUserRoleResponse> ChangeUserRole([FromBody] ChangeUserRoleRequest request) => await _mediator.Send(request);

        [AdminAccess]
        [HttpDelete]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<DeleteUserResponse> DeleteUser([FromQuery] DeleteUserRequest deleteUserRequest) => _mediator.Send(deleteUserRequest);

        [HttpPost]
        [ProducesResponseType(typeof(RegisterNewUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<RegisterNewUserResponse> RegisterUser([FromBody] RegisterNewUserRequest registerUserRequest) => _mediator.Send(registerUserRequest);

    }
}
