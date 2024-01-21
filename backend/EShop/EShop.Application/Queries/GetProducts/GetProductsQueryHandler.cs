using EShop.Contracts;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.RequestFeatures;
using MediatR;

namespace EShop.Application.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductIndexDto>>
{
    private readonly IServiceManager _serviceManager;

    public GetProductsQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<PaginatedList<ProductIndexDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _serviceManager.ProductService.GetAllProductsAsync(request.PagedProduct ?? new PagedProduct());

        return products;
    }
}
