using EShop.Api.Controllers;
using EShop.Application.Commands.CreateUser;
using EShop.Application.Commands.DeleteUser;
using EShop.Application.Commands.UpdateUser;
using EShop.Application.Queries.GetUser;
using EShop.Application.Queries.GetUsers;
using EShop.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;
using EShop.Shared.DataTransferObjects.UserDtos;
using EShop.Shared.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Test.Controllers;

public class UsersContollerTests
{
    private Mock<ISender> _senderMock;
    private UsersController _controller;
    private readonly UserIndexDto _userDto;

    public UsersContollerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new UsersController(_senderMock.Object);
        _userDto = new UserIndexDto("1", "Jhonny Smith", "jsmith@hotmail.com");
    }

    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsListOfUsers()
    {
        // Arrange
        _senderMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), default))
                  .ReturnsAsync(new List<UserIndexDto>()
                  {
                      new UserIndexDto("1", "Jhonny Smith", "jsmith@hotmail.com"),
                      new UserIndexDto("2", "Jhonny Smith", "jsmith@hotmail.com"),
                      new UserIndexDto("3", "Jhonny Smith", "jsmith@hotmail.com"),
                      new UserIndexDto("4", "Jhonny Smith", "jsmith@hotmail.com"),
                      new UserIndexDto("5", "Jhonny Smith", "jsmith@hotmail.com"),
                  });

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _controller.GetUsers();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<GetUsersQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetUser_WhenCalled_ReturnsOneUser()
    {
        // Arrange
        var userId = "1";

        _senderMock.Setup(x => x.Send(It.IsAny<GetUserDetailsQuery>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(_userDto);

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<GetUserDetailsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResult()
    {
        // Arrange
        var userDto = new CreateUserDto("Jhonny Smith", "jsmith@hotmail.com", "Pass123", false);

        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(_userDto);

        // Act
        var result = await _controller.RegisterUser(userDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContentResult()
    {
        // Arrange
        var UserId = "1";
        var updateUser = new UpdateUserDto("Jhonny Smith", "jsmith@hotmail.com");

        _senderMock.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateUser(UserId, updateUser);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContentResult()
    {
        // Arrange
        var UserId = "1";

        _senderMock.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteUser(UserId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _senderMock.Verify(x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
