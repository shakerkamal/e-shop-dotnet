using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;

public class OrderItem
{
    public string? Name { get; set; }

    [BsonRequired]
    public int Qty { get; set; }

    [BsonRequired]
    public double Price { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonRequired]
    public string ProductId { get; set; } = null!;
}
