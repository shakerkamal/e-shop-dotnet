using EShop.Application.Commands.CreateUser;
using EShop.Application.Commands.DeleteUser;
using EShop.Application.Commands.UpdateUser;
using EShop.Application.Queries.GetUser;
using EShop.Application.Queries.GetUsers;
using EShop.Entities.Models;
using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var response = await _sender.Send(new GetUsersQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _sender.Send(new GetUserDetailsQuery(id));
        return Ok(user);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(CreateUserDto user)
    {
        var response = await _sender.Send(new CreateUserCommand(user));
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody]UpdateUserDto updateUser)
    {
        await _sender.Send(new UpdateUserCommand(id, updateUser));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _sender.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
