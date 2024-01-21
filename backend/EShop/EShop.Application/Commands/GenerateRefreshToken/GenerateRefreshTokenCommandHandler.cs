using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;

namespace EShop.Application.Commands.GenerateRefreshToken;

internal sealed class GenerateRefreshTokenCommandHandler : IRequestHandler<GenerateRefreshTokenCommand, TokenDto>
{
    private readonly IServiceManager _serviceManager;

    public GenerateRefreshTokenCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<TokenDto> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var newToken = await _serviceManager.AuthenticationService.RefreshToken(request.Token);
        return newToken;
    }
}
