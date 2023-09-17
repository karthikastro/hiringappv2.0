using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace iTalent.Models
{
    public class PdfDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PdfId { get; set; } = string.Empty;
        [BsonRequired]
        public byte[]? Content { get; set; }
    }
}
