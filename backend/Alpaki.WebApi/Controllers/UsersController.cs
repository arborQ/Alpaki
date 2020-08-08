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

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [VolunteerAccess]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<GetUserResponse> GetCurrentUserProfile() => _mediator.Send(new GetUserRequest { UserId = _currentUserService.CurrentUserId });

        [VolunteerAccess]
        [HttpPatch("me")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<UpdateUserDataResponse> UpdateUserData(UpdateUserDataRequest updateUserDataRequest) => _mediator.Send(updateUserDataRequest);

        [AdminAccess]
        [HttpPatch("role")]
        [ProducesResponseType(typeof(ChangeUserRoleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<ChangeUserRoleResponse> ChangeUserRole(ChangeUserRoleRequest request) => await _mediator.Send(request);

        [AdminAccess]
        [HttpDelete]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<DeleteUserResponse> DeleteUser([FromQuery]DeleteUserRequest deleteUserRequest) => _mediator.Send(deleteUserRequest);

        [VolunteerAccess]
        [HttpGet]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetUsersResponse> GetUsers([FromQuery] GetUsersRequest getUsersRequest) => _mediator.Send(getUsersRequest);

        [VolunteerAccess]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UpdateUserDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetUserResponse> GetUsers([FromRoute] long userId) => _mediator.Send(new GetUserRequest { UserId = userId });
    }
}
