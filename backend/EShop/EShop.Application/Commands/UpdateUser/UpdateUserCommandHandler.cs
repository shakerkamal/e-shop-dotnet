using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;

namespace EShop.Application.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IServiceManager _serviceManager;

    public UpdateUserCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.UserService.GetUserAsync(request.UserId) is null)
            throw new UserNotFoundException(request.UserId);
        await _serviceManager.UserService.UpdateUserAsync(request.UserId, request.UpdateUser);
    }
}
