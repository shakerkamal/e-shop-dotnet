using EShop.Application.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ISender _sender;

        public DocumentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            await _sender.Send(new UploadFileCommand(file));
            return Ok(file.FileName);
        }
    }
}
