using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace iTalent.Models.Response
{
    public class ResponseDTO
    {
        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string MobileNo { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string JobType { get; set; }


        //[BsonRepresentation(BsonType.String)]
        public OverallStatus OverallStatus { get; set; }

        [BsonRequired]
        public string CreatedBy { get; set; }

        [BsonRequired]
        public string UpdatedBy { get; set; }

        public bool AddForLater { get; set; }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }

}
