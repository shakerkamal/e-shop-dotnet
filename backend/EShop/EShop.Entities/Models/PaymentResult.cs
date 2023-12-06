using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;
public class PaymentResult
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Status { get; set; }

    public string? UpdateTime { get; set; }

    public string? EmailAddress { get; set; }
}
