using EShop.Contracts;
using EShop.Entities.Models;
using MongoDB.Driver;

namespace EShop.Repository.Implementations;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
