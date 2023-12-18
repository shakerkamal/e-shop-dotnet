using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;

namespace EShop.Application.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IServiceManager _serviceManager;

    public UpdateProductCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.ProductService.GetProductAsync(request.Id) is null)
            throw new ProductNotFoundException(request.Id);
        await _serviceManager.ProductService.UpdateProductAsync(request.Id, request.UpdateProduct);
    }
}
