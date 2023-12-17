using EShop.Contracts;
using EShop.Entities.Models;
using MongoDB.Driver;

namespace EShop.Repository.Implementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
