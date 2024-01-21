using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Queries.GetProduct;

internal sealed class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>
{
    private readonly IServiceManager _serviceManager;

    public GetProductDetailsQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);
        return product;
    }
}
