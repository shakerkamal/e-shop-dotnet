using EShop.Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderIndexDto>> GetAllOrdersAsync();
    Task<OrderIndexDto> GetOrderAsync(string orderId);
    Task<OrderIndexDto> CreateOrderAsync(CreateOrderDto order);
    Task<IEnumerable<OrderIndexDto>> GetByIdsAsync(IEnumerable<string> ids);
    Task<(IEnumerable<OrderIndexDto> orders, string ids)> CreateOrderCollectionAsync(IEnumerable<CreateOrderDto> orderCollection);
    Task DeleteOrderAsync(string orderId);
    Task UpdateOrderAsync(string orderid, UpdateOrderDto orderForUpdate);
}
