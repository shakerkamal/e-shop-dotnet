using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;

namespace EShop.Application.Commands.GenerateRefreshToken;

public sealed record GenerateRefreshTokenCommand(TokenDto Token) : IRequest<TokenDto>;
