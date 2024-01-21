using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;

namespace EShop.Application.Queries.GetOrder;

internal sealed class GetOrderQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderIndexDto>
{
    private readonly IServiceManager _serviceManager;

    public GetOrderQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<OrderIndexDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _serviceManager.OrderService.GetOrderAsync(request.Id);
        if (order is null)
            throw new OrderNotFoundException(request.Id);
        return order;
    }
}
