namespace EShop.Shared.DataTransferObjects.OrderDtos;

public record OrderIndexDto(string Id, string? PaymentMethod, double TaxPrice, double ShippingPrice, double TotalPrice, bool IsDelivered, bool IsPaid);

public record ShippingAddressDto(string? Address, string? City, string? PostalCode, string? Country);
