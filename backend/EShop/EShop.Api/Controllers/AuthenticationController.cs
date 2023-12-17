using EShop.Application.Commands.CreateUser;
using EShop.Application.Commands.GetUsers;
using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;
        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto user)
        {
            var response = await _sender.Send(new CreateUserCommand(user));
            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _sender.Send(new GetUsersQuery());
            return Ok(response);
        }
    }
}
