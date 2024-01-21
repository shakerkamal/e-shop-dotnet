using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;

namespace EShop.Application.Commands.CreateOrder;

public sealed record CreateOrderCommand(CreateOrderDto CreateOrder) : IRequest<OrderIndexDto>;
