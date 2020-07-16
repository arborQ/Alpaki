using System;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.WebApi.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("gl")]
    public class GraphQLController : ControllerBase
    {
        private readonly AdminDreamerQuery _adminDreamerQuery;
        private readonly VolunteerDreamerQuery _volunteerDreamerQuery;
        private readonly CoordinatorDreamerQuery _coordinatorDreamerQuery;
        private readonly ICurrentUserService _currentUserService;

        public GraphQLController(AdminDreamerQuery adminDreamerQuery, VolunteerDreamerQuery volunteerDreamerQuery, CoordinatorDreamerQuery coordinatorDreamerQuery, ICurrentUserService currentUserService)
        {
            _adminDreamerQuery = adminDreamerQuery;
            _volunteerDreamerQuery = volunteerDreamerQuery;
            _coordinatorDreamerQuery = coordinatorDreamerQuery;
            _currentUserService = currentUserService;
        }

        private DreamerQuery ResolveUserGraphQLSchema()
        {
            if (_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Admin))
            {
                return _adminDreamerQuery;
            }
            else if (_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Coordinator))
            {
                return _coordinatorDreamerQuery;
            }
            else if (_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Volunteer))
            {
                return _volunteerDreamerQuery;
            }
            else
            {
                throw new Exception("Can't resolve GraphQL Query");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {

            var schema = new Schema
            {
                Query = ResolveUserGraphQLSchema()
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
