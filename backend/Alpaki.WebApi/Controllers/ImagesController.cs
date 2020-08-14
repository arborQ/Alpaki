using System.IO;
using System.Net;
using System.Threading.Tasks;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using Alpaki.Logic.Handlers.AssignVolunteer;
using Alpaki.WebApi.Policies;
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
        //[ProducesResponseType(typeof(RegisterVolunteerResponse), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task AddTemporaryFile(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await formFile.CopyToAsync(stream);
            }
        }
    }
}
