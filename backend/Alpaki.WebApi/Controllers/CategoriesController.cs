using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AddCategory;
using Alpaki.Logic.Handlers.GetCategories;
using Alpaki.Logic.Handlers.UpdateCategory;
using Alpaki.WebApi.Policies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public Task<AddCategoryResponse> CreateCategory([FromBody] AddCategoryRequest createCategoryRequest) => _mediator.Send(createCategoryRequest);

        [CoordinatorAccess]
        [HttpPatch]
        [ProducesResponseType(typeof(UpdateCategoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<UpdateCategoryResponse> UpadteCategory([FromBody]UpdateCategoryRequest updateCategoryRequest) => _mediator.Send(updateCategoryRequest);

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(GetCategoriesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Forbidden)]
        public Task<GetCategoriesResponse> GetCategories([FromQuery]GetCategoriesRequest getCategoriesRequest) => _mediator.Send(getCategoriesRequest);
    }
}
