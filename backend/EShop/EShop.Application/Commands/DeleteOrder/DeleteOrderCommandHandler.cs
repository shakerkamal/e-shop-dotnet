using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;

namespace EShop.Application.Commands.DeleteOrder;

internal sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IServiceManager _serviceManager;

    public DeleteOrderCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.OrderService.GetOrderAsync(request.Id) is null)
            throw new OrderNotFoundException(request.Id);

        await _serviceManager.OrderService.DeleteOrderAsync(request.Id);
    }
}
