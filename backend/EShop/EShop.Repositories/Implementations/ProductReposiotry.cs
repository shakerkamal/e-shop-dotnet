using EShop.Contracts;
using EShop.Entities.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementations;

public class ProductReposiotry : RepositoryBase<Product>, IProductRepository
{
    public ProductReposiotry(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
