using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Threading.Tasks;
using iTalent.Models;

namespace iTalent.Controllers
{
    //[ApiController]
    [Route("api/pdf")]
    public class PdfDocumentController : ControllerBase
    {
        private readonly IMongoCollection<PdfDocument> _collection;

        public PdfDocumentController(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("conn"));
            var database = client.GetDatabase("HiringAppDb");
            _collection = database.GetCollection<PdfDocument>("PdfCollection");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var pdfDocument = new PdfDocument
                    {

                        Content = ms.ToArray()
                    };
                    _collection.InsertOne(pdfDocument);
                }

                return Ok("PDF uploaded and stored in MongoDB.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
