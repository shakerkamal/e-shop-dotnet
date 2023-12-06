using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;
public class ShippingAddress
{
    [BsonRequired]
    public string? Address { get; set; }

    [BsonRequired]
    public string? City { get; set; }

    [BsonRequired]
    public string? PostalCode { get; set; }

    [BsonRequired]
    public string? Country { get; set; }
}
