using EShop.Contracts;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.OrderDtos;
using MongoDB.Bson;

namespace EShop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public OrderService(IRepositoryManager repositoryManager, 
                        ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    public async Task<IEnumerable<OrderIndexDto>> GetAllOrdersAsync()
    {
        var orderDocuments = await _repositoryManager.Order.GetAllAsync();

        var orderDtos = MapOrdersToOrderDto(orderDocuments);
        return orderDtos;
    }

    public async Task<OrderIndexDto> GetOrderAsync(string orderId)
    {
        var order = await _repositoryManager.Order.GetAsync(orderId);
        var orderDto = MapOrderToOrderIndexDto(order);
        return orderDto;
    }

    public async Task<OrderIndexDto> CreateOrderAsync(CreateOrderDto order)
    {
        var orderDocument = MapOrderDtoToOrder(order);
        await _repositoryManager.Order.AddAsync(orderDocument);
        var orderIndex = MapOrderToOrderIndexDto(orderDocument);
        return orderIndex;
    }

    public Task<(IEnumerable<OrderIndexDto> orders, string ids)> CreateOrderCollectionAsync(IEnumerable<CreateOrderDto> orderCollection)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        var objectId = new ObjectId(orderId);
        await _repositoryManager.Order.DeleteAsync(u => u.Id == objectId);
    }

    public Task<IEnumerable<OrderIndexDto>> GetByIdsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrderAsync(string orderId, UpdateOrderDto orderForUpdate)
    {
        var orderDocument = await _repositoryManager.Order.GetAsync(orderId);
        var updatedOrderDocument = MapOrderDtoToOrder(orderDocument, orderForUpdate);

        _repositoryManager.Order.Update(updatedOrderDocument);
    }


    #region private methods

    private List<OrderIndexDto> MapOrdersToOrderDto(IEnumerable<Order> orderDocuments)
    {
        var orderDtos = new List<OrderIndexDto>();
        foreach (var order in orderDocuments)
        {
            var orderDto = MapOrderToOrderIndexDto(order);
            orderDtos.Add(orderDto);
        }
        return orderDtos;
    }

    private OrderIndexDto MapOrderToOrderIndexDto(Order order)
    {
        return new OrderIndexDto(
                order.Id.ToString(),
                order.PaymentMethod,
                order.TaxPrice,
                order.ShippingPrice,
                order.TotalPrice,
                order.IsDelivered,
                order.IsPaid
                );
    }

    private Order MapOrderDtoToOrder(CreateOrderDto orderDto)
    {
        var order = new Order
        {
            IsDelivered = orderDto.IsDelivered,
            PaidAt = orderDto.PaidAt,
            DeliveredAt = orderDto.DeliveredAt,
            ShippingAddress = MapShippingDtoToShipping(orderDto.ShippingAddress),
            OrderItems = MapOrderItemsDtoToOrderItems(orderDto.OrderItems),
            PaymentMethod = orderDto.PaymentMethod,
            PaymentResult = MapPaymentDtoToPayment(orderDto.PaymentResult),
            ShippingPrice = orderDto.ShippingPrice,
            TaxPrice = orderDto.TaxPrice,
            TotalPrice = orderDto.TotalPrice,
            UserId = orderDto.UserId,
            IsPaid = orderDto.IsPaid,
        };

        return order;
    }

    private Order MapOrderDtoToOrder(Order oldOrderDocument, UpdateOrderDto orderDto)
    {
        oldOrderDocument.IsDelivered = orderDto.IsDelivered;
        oldOrderDocument.PaidAt = orderDto.PaidAt;
        oldOrderDocument.DeliveredAt = orderDto.DeliveredAt;
        oldOrderDocument.ShippingAddress = MapShippingDtoToShipping(orderDto.ShippingAddress);
        oldOrderDocument.OrderItems = MapOrderItemsDtoToOrderItems(orderDto.OrderItems);
        oldOrderDocument.PaymentMethod = orderDto.PaymentMethod;
        oldOrderDocument.PaymentResult = MapPaymentDtoToPayment(orderDto.PaymentResult);
        oldOrderDocument.ShippingPrice = orderDto.ShippingPrice;
        oldOrderDocument.TaxPrice = orderDto.TaxPrice;
        oldOrderDocument.TotalPrice = orderDto.TotalPrice;
        oldOrderDocument.UserId = orderDto.UserId;
        oldOrderDocument.IsPaid = orderDto.IsPaid;

        return oldOrderDocument;
    }

    private PaymentResult MapPaymentDtoToPayment(PaymentResultDto? paymentResultDto)
    {
        var paymentResult = new PaymentResult
        {
            Status = paymentResultDto?.Status,
            EmailAddress = paymentResultDto?.EmailAddress,
            UpdateTime = paymentResultDto?.UpdateTime,
        };

        return paymentResult;
    }

    private List<OrderItem> MapOrderItemsDtoToOrderItems(List<OrderItemDto> orderItems)
    {
        var items = new List<OrderItem>();
        foreach (var item in orderItems)
        {
            var orderItem = new OrderItem
            {
                Name = item.Name,
                Price = item.Price,
                ProductId = item.ProductId,
                Qty = item.Qty,
            };
            items.Add(orderItem);
        };
        return items;
    }

    private ShippingAddress MapShippingDtoToShipping(ShippingAddressDto shippingAddressDto)
    {
        var shippingAddress = new ShippingAddress
        {
            Address = shippingAddressDto.Address,
            City = shippingAddressDto.City,
            PostalCode = shippingAddressDto.PostalCode,
            Country = shippingAddressDto.Country,
        };

        return shippingAddress;
    }

    #endregion
}
