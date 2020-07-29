using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AddCategory;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [CoordinatorAccess]
        [HttpPost]
        [ProducesResponseType(typeof(AddCategoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<AddCategoryResponse> UpdateUserData(AddCategoryRequest updateUserDataRequest) => _mediator.Send(updateUserDataRequest);
    }
}
