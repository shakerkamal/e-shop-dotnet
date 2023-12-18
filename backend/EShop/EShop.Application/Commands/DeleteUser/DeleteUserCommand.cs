using MediatR;

namespace EShop.Application.Commands.DeleteUser;

public sealed record DeleteUserCommand(string Id) : IRequest;
