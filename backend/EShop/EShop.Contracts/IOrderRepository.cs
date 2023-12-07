using EShop.Entities.Models;

namespace EShop.Contracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<PaginatedList<Order>> GetAllOrdersAsync(bool trackChanges);
    Task<Order> GetOrderAsync(Guid orderId, bool trackChanges);
    Task CreateOrderAsync(Order order);
    void DeleteOrder(Order order);
    void UpdateOrder(Order order);
}
