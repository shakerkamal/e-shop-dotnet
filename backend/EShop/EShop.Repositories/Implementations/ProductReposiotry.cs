using EShop.Contracts;
using EShop.Entities.Models;
using MongoDB.Driver;

namespace EShop.Repository.Implementations;

public class ProductReposiotry : RepositoryBase<Product>, IProductRepository
{
    public ProductReposiotry(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
