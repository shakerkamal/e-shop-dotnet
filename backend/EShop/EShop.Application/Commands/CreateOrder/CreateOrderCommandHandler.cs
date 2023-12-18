using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.CreateOrder;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderIndexDto>
{
    private readonly IServiceManager _serviceManager;

    public CreateOrderCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<OrderIndexDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var response = await _serviceManager.OrderService.CreateOrderAsync(request.CreateOrder);
        return response;
    }
}
