using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserIndexDto>
{
    private readonly IServiceManager _serviceManager;

    public CreateUserCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    public async Task<UserIndexDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _serviceManager.UserService.CreateUserAsync(request.CreateUser);
        return response;
    }
}
