using EShop.Application.Commands.AuthenticateUser;
using EShop.Application.Commands.GenerateRefreshToken;
using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;
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

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]AuthenticateUserDto authenticateUser)
        {
            var token = await _sender.Send(new AuthenticateUserCommand(authenticateUser));
            return Ok(token);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody]TokenDto token)
        {
            var response = await _sender.Send(new GenerateRefreshTokenCommand(token));
            return Ok(response);
        }

    }
}
