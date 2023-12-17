namespace EShop.Contracts;

public interface IMongoDbSettings
{
    string DatabaseName { get; }
    string ConnectionString { get; }
}
