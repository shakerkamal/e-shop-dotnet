using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;

namespace EShop.Application.Commands.AuthenticateUser;

public sealed record AuthenticateUserCommand(AuthenticateUserDto Authenticate) : IRequest<TokenDto>;
