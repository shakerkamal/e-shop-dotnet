using EShop.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using EShop.Shared.RequestFeatures;
using MediatR;

namespace EShop.Application.Queries.GetOrders;

public sealed record GetOrdersQuery(PagedOrder PagedOrder) : IRequest<PaginatedList<OrderIndexDto>>;
