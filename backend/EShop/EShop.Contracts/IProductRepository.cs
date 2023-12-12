using EShop.Entities.Models;

namespace EShop.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<PaginatedList<Product>> GetAllProductsAsync(bool trackChanges);
    Task<Product> GetProductAsync(string productId, bool trackChanges);
    Task CreateProductAsync(Product product);
    void DeleteProduct(Product product);
    void UpdateProduct(Product product);
}
