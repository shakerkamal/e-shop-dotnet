using EShop.Entities.Models;

namespace EShop.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
    Task<Product> GetProductAsync(Guid productId, bool trackChanges);
    Task CreateProductAsync(Product product);
    void DeleteProduct(Product product);
    void UpdateProduct(Product product);
}
