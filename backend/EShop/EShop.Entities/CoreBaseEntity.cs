using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Entities;

public abstract class CoreBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; set; }

    //public DateTime CreatedAt => Id.CreationTime;
}
