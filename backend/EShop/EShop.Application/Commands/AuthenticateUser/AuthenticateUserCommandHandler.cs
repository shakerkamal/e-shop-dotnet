using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;

namespace EShop.Application.Commands.AuthenticateUser;

internal sealed class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, TokenDto>
{
    private readonly IServiceManager _serviceManager;

    public AuthenticateUserCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<TokenDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _serviceManager.AuthenticationService.Authenticate(request.Authenticate))
            throw new UnauthorizedAccessException();
        var token = await _serviceManager.AuthenticationService.CreateToken(true);
        return token;
    }
}
