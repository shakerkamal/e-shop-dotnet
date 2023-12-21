using EShop.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using EShop.Shared.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Queries.GetOrders;

public sealed record GetOrdersQuery(PagedOrder PagedOrder) : IRequest<PaginatedList<OrderIndexDto>>;
