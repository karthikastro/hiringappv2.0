using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace iTalent.Models.Request
{

    public class PostCandidateDTO 
    {


        [BsonRequired]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        [Required(ErrorMessage = "MobileNo is required.")]
        public string MobileNo { get; set; }

        [BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "JobType is required.")]
        public string JobType { get; set; }

        [Required(ErrorMessage = "OverallStatus is required.")]
        [BsonRepresentation(BsonType.String)]
        public OverallStatus OverallStatus { get; set; } =0;

        [BsonRequired]
        [Required(ErrorMessage = "CreatedBy is required.")]
        public string CreatedBy { get; set; }

        [BsonRequired]
        [Required(ErrorMessage = "UpdatedBy is required.")]
        public string UpdatedBy { get; set; }
        [BsonRequired]
        [Required(ErrorMessage = "AddForLater is required.")]
        public bool AddForLater { get; set; } =false;



    }



    public class UpdateCandidteDTO
    {

       
        [BsonRepresentation(BsonType.String)]
        public OverallStatus OverallStatus { get; set; }

        public string? UpdatedBy { get; set; }


        public bool? AddForLater { get; set; }

    }


}
