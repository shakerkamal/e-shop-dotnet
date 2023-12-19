using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;

namespace EShop.Application.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductIndexDto>>
{
    private readonly IServiceManager _serviceManager;

    public GetProductsQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IEnumerable<ProductIndexDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _serviceManager.ProductService.GetAllProductsAsync();

        return products;
    }
}
