namespace EShop.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IOrderRepository Order { get; }
    Task SaveChangesAsync();
    ValueTask DisposeAsync();
}
