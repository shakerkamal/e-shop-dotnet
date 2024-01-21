using MediatR;

namespace EShop.Application.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(string Id) : IRequest;
