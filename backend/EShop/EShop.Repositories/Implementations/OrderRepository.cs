using EShop.Contracts;
using EShop.Entities.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementations;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
