using EShop.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private IMongoDatabase _database;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private bool _disposed;

        public RepositoryManager(IMongoDbSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName); ;
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

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //await _database.DisposeAsync();
                }
                _disposed = true;
            }
        }
    }
}
