using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace App.Infrastructure.Documents;

public class BookDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }

    public string OwnerId { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public decimal SalesPrice { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? SentDate { get; set; }
    public DateTime? ActivateDate { get; set; }
    public string SalesState { get; set; }
    public string ApprovedBy { get; set; }
    public List<string> Comments { get; set; }
}
