using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;

namespace EShop.Application.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(string Id, UpdateOrderDto UpdateOrder) : IRequest;
