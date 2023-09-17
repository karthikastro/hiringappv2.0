using iTalent.Models;
using iTalent.Models.Response;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Globalization;
using System.Linq;

namespace iTalent.Services
{
    public class CandidatesServices : ICandidatesServices
    {
        private readonly IMongoCollection<Candidates> _candidates;

    public CandidatesServices(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _candidates = database.GetCollection<Candidates>(settings.CollectionName);
        }

        public Candidates Create(Candidates candidate)
        {
            
                _candidates.InsertOne(candidate);
                return candidate;
            
           
        }
        

        public List<Candidates> Get(string? sortBy, string? fieldName,string? overallStatus, string? filter,string? orderBy,DateTime? startDate,DateTime? endDate)
        {
            var currentDateTime = DateTime.Now;
          

            var pipeline = new List<BsonDocument>();

            var pipeline2 = new List<BsonDocument>();

            // Filter based on your criteria (e.g., filter by a field named "Category")
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(fieldName))
            {
               
                
                var filterStage = new BsonDocument("$match", new BsonDocument(fieldName, filter));
                pipeline.Add(filterStage);
            }

            
            //filter based on filter date values
            if(startDate != null && endDate != null) 
            {
                var fielddatespec = fieldName;
                if (fielddatespec != "CreatedDate" || fielddatespec != "UpdatedDate")
                {
                    fielddatespec = "CreatedDate";
                }
                var daterangefilterstage = new BsonDocument("$match", new BsonDocument(fielddatespec,
                            new BsonDocument{
                                    {"$gte",startDate},
                                    {"$lte",endDate}
                                    }
                            ));
                pipeline.Add(daterangefilterstage);

            }

            //filter based on overall status
            if (!string.IsNullOrEmpty(overallStatus))
            {
                var overallStatusCaps = overallStatus.ToUpper();
                var statusfilterstage = new BsonDocument("$match", new BsonDocument("OverallStatus", overallStatusCaps));
                pipeline.Add(statusfilterstage);

            }

            // Sort based on a field (e.g., sort by "Name")
            if (!string.IsNullOrEmpty(sortBy))
            {

               
                
                    var sortStage = new BsonDocument("$sort", new BsonDocument(sortBy, 1)); // 1 for ascending, -1 for descending

                    pipeline.Add(sortStage);
                

            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                orderBy = orderBy.ToLower();
                if (orderBy == "asc")
                {
                    var sortStage = new BsonDocument("$sort", new BsonDocument(sortBy, 1));
                    pipeline.Add(sortStage);
                }
                else if (orderBy == "desc")
                {
                    var sortStage2 = new BsonDocument("$sort", new BsonDocument(sortBy, -1));
                    pipeline.Add(sortStage2);
                }
                else
                {
                   
                    var recenttime = currentDateTime.AddHours(-24);
                    var current2 = currentDateTime.AddDays(-2);
                    var fielddatespec = fieldName;
                    if (fielddatespec != "CreatedDate" || fielddatespec != "UpdatedDate")
                    {
                        fielddatespec = "CreatedDate";
                    }

                    var daterangestage1 = new BsonDocument("$match", new BsonDocument(fielddatespec,
                            new BsonDocument{
                                    {"$gte",recenttime},
                                    {"$lt",currentDateTime}
                                    }
                            ));
                    pipeline.Add(daterangestage1);

                    var daterangestage2 = new BsonDocument("$sort", new BsonDocument(fielddatespec, -1)); // 1 for ascending, -1 for descending

                    pipeline.Add(daterangestage2);


                    var filterStage2 = BsonDocument.Parse("{ $match: { CreatedDate: { $lt: ISODate('" + current2.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "') } } }");
                    pipeline2.Add(filterStage2);

                    //return _candidates.Find(candidate => true).ToList();
                }

                
            }

           

            var aggregationOptions = new AggregateOptions { AllowDiskUse = false };
            var recentresult = _candidates.Aggregate<Candidates>(pipeline, aggregationOptions).ToList();
            var oldresult = _candidates.Aggregate<Candidates>(pipeline2, aggregationOptions).ToList();
            var result = recentresult;
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
            if (orderBy != "asc" && orderBy != "desc" && orderBy != null)
            {
                result = recentresult.Concat(oldresult).ToList();
            }

            foreach (var item in oldresult)
            {
                Console.WriteLine("pipeline 2   ....... ");
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("end ....... ");
            return result;
        }

        public Candidates Get(string id)
        {
           
            return _candidates.Find(candidate => candidate.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _candidates.DeleteOne(candidate => candidate.Id == id);
        }

        public void Update(string id, Candidates candidate)
        {
           // _candidates.UpdateOne(id, candidate, null);   
            _candidates.ReplaceOne(candidate => candidate.Id == id, candidate);
        }
    }

}
