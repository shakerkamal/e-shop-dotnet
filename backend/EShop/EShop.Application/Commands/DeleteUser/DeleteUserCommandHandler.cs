using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;

namespace EShop.Application.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IServiceManager _serviceManager;

    public DeleteUserCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.UserService.GetUserAsync(request.Id) is null)
            throw new UserNotFoundException(request.Id);

        await _serviceManager.UserService.DeleteUserAsync(request.Id);
    }
}
