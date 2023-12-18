namespace EShop.Shared.DataTransferObjects.OrderDtos;

public record OrderDetailsDto(
    string Id, 
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
    DateTime? DeliveredAt);


public record OrderItemDto(string? Name, int Qty, double Price, string ProductId);

public record PaymentResultDto(string? Status, string? UpdateTime, string? EmailAddress);