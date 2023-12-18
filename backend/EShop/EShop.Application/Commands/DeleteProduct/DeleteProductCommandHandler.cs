using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IServiceManager _serviceManager;

    public DeleteProductCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (await _serviceManager.ProductService.GetProductAsync(request.Id) is null)
            throw new ProductNotFoundException(request.Id);

        await _serviceManager.ProductService.DeleteProductAsync(request.Id);
    }
}
