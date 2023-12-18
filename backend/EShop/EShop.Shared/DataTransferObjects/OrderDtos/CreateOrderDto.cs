using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DataTransferObjects.OrderDtos;

public record CreateOrderDto(
    List<OrderItemDto> OrderItems,
    ShippingAddressDto ShippingAddress,
    string? PaymentMethod,
    PaymentResultDto? PaymentResult,
    double TaxPrice,
    double ShippingPrice,
    double TotalPrice,
    bool IsPaid,
    DateTime? PaidAt,
    bool IsDelivered,
    DateTime? DeliveredAt,
    string UserId);
