using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;

namespace EShop.Application.Queries.GetOrder;

public sealed record GetOrderDetailsQuery(string Id) : IRequest<OrderIndexDto>;
