using EShop.Shared.DataTransferObjects.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductIndexDto>> GetAllProductsAsync();
    Task<ProductIndexDto> GetProductAsync(Guid productId);
    Task<ProductIndexDto> CreateProductAsync(CreateProductDto product);
    Task<IEnumerable<ProductIndexDto>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<(IEnumerable<ProductIndexDto> products, string ids)> CreateProductCollectionAsync(IEnumerable<CreateProductDto> productCollection);
    Task DeleteProductAsync(Guid productId);
    Task UpdateProductAsync(Guid productid, UpdateProductDto productForUpdate);
}
