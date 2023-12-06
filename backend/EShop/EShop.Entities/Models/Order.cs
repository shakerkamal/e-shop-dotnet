using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;
public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonRequired]
    public string UserId { get; set; } = null!;

    public List<OrderItem>? OrderItems { get; set; }

    public ShippingAddress? ShippingAddress { get; set; }

    [BsonRequired]
    public string? PaymentMethod { get; set; }

    public PaymentResult? PaymentResult { get; set; }

    [BsonRequired]
    public double TaxPrice { get; set; } = 0.0;

    [BsonRequired]
    public double ShippingPrice { get; set; } = 0.0;

    [BsonRequired]
    public double TotalPrice { get; set; } = 0.0;

    [BsonRequired]
    public bool IsPaid { get; set; } = false;

    public DateTime? PaidAt { get; set; }

    [BsonRequired]
    public bool IsDelivered { get; set; } = false;

    public DateTime? DeliveredAt { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}