using EShop.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using EShop.Shared.RequestFeatures;

namespace EShop.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductIndexDto>> GetAllProductsAsync();
    Task<PaginatedList<ProductIndexDto>> GetAllProductsAsync(PagedProduct pagedProduct);
    Task<ProductDetailsDto> GetProductAsync(string productId);
    Task<ProductIndexDto> CreateProductAsync(CreateProductDto product);
    Task<IEnumerable<ProductIndexDto>> GetByIdsAsync(IEnumerable<string> ids);
    Task<(IEnumerable<ProductIndexDto> products, string ids)> CreateProductCollectionAsync(IEnumerable<CreateProductDto> productCollection);
    Task DeleteProductAsync(string productId);
    Task UpdateProductAsync(string productid, UpdateProductDto productForUpdate);
}
