namespace EShop.Entities.Models;

[BsonCollection("payments")]
public class PaymentResult : CoreBaseEntity
{
    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    //public string? Id { get; set; }

    public string? Status { get; set; }

    public string? UpdateTime { get; set; }

    public string? EmailAddress { get; set; }
}
