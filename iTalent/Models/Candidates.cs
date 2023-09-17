using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace iTalent.Models
{
    
        public enum OverallStatus
        {
            PENDING,
            INPROGRESS,
            SELECTED,
            REJECTED
        }

        public class Candidates
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; } = String.Empty;

            public string Name { get; set; }

            [BsonRequired]
            public string Email { get; set; }

            [BsonRequired]
            
           
            public string MobileNo { get; set; }

            [BsonRepresentation(BsonType.String)]
            public string JobType { get; set; }
            //public void getjobtype(JobTypes jobtype)
            //{
            //     JobType = jobtype.JobName;
            //}


            [BsonRepresentation(BsonType.String)]
            public OverallStatus OverallStatus { get; set; }
            [BsonRequired]
            public string CreatedBy { get; set; }
            [BsonRequired]
            public string UpdatedBy { get; set; }


            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
            public bool? AddForLater { get; set; }
        }
    
}
