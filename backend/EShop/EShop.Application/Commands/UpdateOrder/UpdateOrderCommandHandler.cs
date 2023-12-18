using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.UpdateOrder;

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IServiceManager _serviceManager;

    public UpdateOrderCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.OrderService.GetOrderAsync(request.Id) is null)
            throw new OrderNotFoundException(request.Id);
        await _serviceManager.OrderService.UpdateOrderAsync(request.Id, request.UpdateOrder);
    }
}
