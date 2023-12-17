using EShop.Contracts;
using MongoDB.Driver;

namespace EShop.Repository.Implementations;

public class RepositoryManager : IRepositoryManager
{
    private IMongoDatabase _database;
    private IProductRepository _productRepository;
    private IOrderRepository _orderRepository;
    private IUserRepository _userRepository;

    public RepositoryManager(IMongoDbSettings settings)
    {
        _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
    }
    public IProductRepository Product
    { 
        get 
        {
            if (_productRepository is null)
                _productRepository = new ProductReposiotry(_database);
            return _productRepository; 
        } 
    }

    public IOrderRepository Order
    {
        get
        {
            if (_orderRepository is null)
                _orderRepository = new OrderRepository(_database);
            return _orderRepository;
        }
    }

    public IUserRepository User
    {
        get
        {
            if (_userRepository is null)
                _userRepository = new UserRepository(_database);
            return _userRepository;
        }
    }
}
