using iTalent.Models;
using iTalent.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using iTalent.Models.Response;
using iTalent.Models.Request;

namespace iTalent.Controllers
{
    [ApiController]
    [Route("api/v1/candidate")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesServices _candidateServices;


        public CandidatesController(ICandidatesServices candidateServices)
        {
            _candidateServices = candidateServices;
        }
        [HttpGet]
        public ActionResult<List<Candidates>> Get(string? sortBy, string? fieldName, string? overallStatus, string? filter, string? orderBy, DateTime? startDate, DateTime? endDate)
        {
            try
            {

                return _candidateServices.Get(sortBy, fieldName, overallStatus, filter, orderBy, startDate, endDate);
        
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                var candidate = _candidateServices.Get(id);
                if (candidate == null)
                {
                    return NotFound($"Student with id : {id} not found");
                }
                return Ok(candidate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] PostCandidateDTO candidateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errorMessage = "Invalid input. Please check your data.";
                    throw new ValidationException(errorMessage);
                }


                Candidates candidate = new Candidates();
                

                candidate.Name = candidateDTO.Name;
                candidate.JobType = candidateDTO.JobType;
                candidate.CreatedBy = candidateDTO.CreatedBy;
                candidate.UpdatedBy = candidateDTO.UpdatedBy;
                candidate.Email = candidateDTO.Email;
                candidate.MobileNo = candidateDTO.MobileNo;
                candidate.OverallStatus = candidateDTO.OverallStatus;
                candidate.CreatedDate = DateTime.Now;
                candidate.UpdatedDate = DateTime.Now;
                candidate.AddForLater = candidateDTO.AddForLater;
                _candidateServices.Create(candidate);

                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }

        }
        [HttpPatch("{id}")]
        public ActionResult Put(string id, [FromBody] UpdateCandidteDTO updcandidate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errorMessage = "Invalid input. Please check your data.";
                    throw new ValidationException(errorMessage);
                }
                var existcandidate = _candidateServices.Get(id);
                if (existcandidate == null)
                {
                    return NotFound("candidate not found");
                }


                Candidates candidate = new Candidates();
                candidate.Id = id;
                //candidate.Name = updcandidate.Name != null ? updcandidate.Name : existcandidate.Name;
                //candidate.Email = updcandidate.Email != null ? updcandidate.Email : existcandidate.Email;
                //candidate.MobileNo = updcandidate.MobileNo != null? updcandidate.MobileNo : existcandidate.MobileNo;
                //candidate.JobType = updcandidate.JobType != null ? updcandidate.JobType : existcandidate.JobType;
                candidate.OverallStatus = updcandidate.OverallStatus != 0 ? updcandidate.OverallStatus : existcandidate.OverallStatus;
                candidate.CreatedBy = existcandidate.CreatedBy;
                candidate.CreatedDate = existcandidate.CreatedDate;
                candidate.UpdatedDate = DateTime.Now;
                candidate.UpdatedBy = updcandidate.UpdatedBy != null ? updcandidate.UpdatedBy : existcandidate.UpdatedBy;
                candidate.AddForLater = updcandidate.AddForLater != null ? updcandidate.AddForLater : existcandidate.AddForLater;

                candidateServices.Update(id, candidate);

                return Ok("Updation Sucessful");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes .Status422UnprocessableEntity, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var existcandidate = _candidateServices.Get(id);
                if (existcandidate == null)
                {
                    return NotFound("candidate not found");
                }
                candidateServices.Remove(id);
                return Ok($"Candidate with id :{id}    is deleted ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
