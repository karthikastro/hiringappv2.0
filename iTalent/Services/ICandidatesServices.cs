using iTalent.Models;

namespace iTalent.Services
{
    

        public interface ICandidatesServices
        {
            List<Candidates> Get(string? sortBy,string? fieldName,string? overallStatus, string? filter,string? orderBy,DateTime? startDate, DateTime? endDate);
            Candidates Get(String id);
            Candidates Create(Candidates candidate);
            void Update(string id, Candidates candidate);
            void Remove(string id);



        }
    
}
