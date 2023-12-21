﻿using EShop.Contracts;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderIndexDto>>
{
    private readonly IServiceManager _serviceManager;

    public GetOrdersQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<PaginatedList<OrderIndexDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _serviceManager.OrderService.GetAllOrdersAsync(request.PagedOrder);
        return orders;
    }
}
