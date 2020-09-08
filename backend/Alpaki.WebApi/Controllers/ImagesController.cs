using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Handlers.AddTemporaryImage;
using Alpaki.Logic.Handlers.GetProfileImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTemporaryImageResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<AddTemporaryImageResponse> AddTemporaryFile(IFormFile formFile)
        {
            using (var fileStream = formFile.OpenReadStream())
            {
                var bytes = new byte[formFile.Length];
                fileStream.Read(bytes, 0, (int)formFile.Length);

                return await _mediator.Send(new AddTemporaryImageRequest { ImageData = bytes });
            }
        }

        [Authorize]
        [HttpGet("{ProfileImageId:guid}.png")]
        public async Task<IActionResult> GetProfileImage([FromRoute] GetProfileImageRequest request)
        {
            var imageData = await _mediator.Send(request);

            return File(imageData.ImageData, "image/png");
        }
    }
}
