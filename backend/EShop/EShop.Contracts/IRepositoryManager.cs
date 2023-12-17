namespace EShop.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IOrderRepository Order { get; }
    IUserRepository User { get; }
}
