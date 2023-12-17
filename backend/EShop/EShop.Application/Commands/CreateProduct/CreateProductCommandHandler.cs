using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductIndexDto>
{
    private readonly IServiceManager _serviceManager;

    public CreateProductCommandHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<ProductIndexDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _serviceManager.ProductService.CreateProductAsync(request.ProductDto);
        return result;
    }
}
