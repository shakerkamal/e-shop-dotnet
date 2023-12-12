using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;

[BsonCollection("users")]
public class User : CoreBaseEntity
{
    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    //public string Id { get; set; } = null!;
    [BsonRequired]
    public string? Name { get; set; }

    [BsonRequired]
    public string? Email { get; set; }

    [BsonRequired]
    public string? Password { get; set; }

    [BsonRequired]
    public bool IsAdmin { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; }
}