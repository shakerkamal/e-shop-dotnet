using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Commands.UpdateUser;

public sealed record UpdateUserCommand(string UserId, UpdateUserDto UpdateUser) : IRequest;
