using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities.Models;
public class Review
{
    public string? Name { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonRequired]
    public string UserId { get; set; } = null!;
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public List<Review>? Reviews { get; set; }
    public double Rating { get; set; }
    public int NumReviews { get; set; }
    public double Price { get; set; }
    public int CountInStock { get; set; }
    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

