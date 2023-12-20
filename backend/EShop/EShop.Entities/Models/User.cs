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
    public string? RefreshToken { get; set; }

    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Representation = BsonType.String)]
    public DateTime RefreshTokenExpiryTime { get; set; }
}