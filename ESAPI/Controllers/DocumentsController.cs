using ESApplication.AggregateModels;
using ESApplication.Commands.Business;
using ESApplication.Commands.Promotion;
using ESApplication.Models;
using ESApplication.Queries.GetBusiness;
using ESApplication.Queries.GetPromotion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public DocumentsController(IMediator mediator, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _mediator = mediator;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Documents  

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            if (!Request.Form.Files.Any())
                return BadRequest("No files found in the request");

            //if (Request.Form.Files.Count > 1)
            //    return BadRequest("Cannot upload more than one file at a time");

            if (Request.Form.Files[0].Length <= 0)
                return BadRequest("Invalid file length, seems to be empty");

            try
            {
                string webRootPath = _hostingEnvironment.ContentRootPath;
                string uploadsDir = Path.Combine(webRootPath, "Resources/Images");

                // wwwroot/uploads/
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;
                foreach (var file in files)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(uploadsDir, fileName);

                    var buffer = 1024 * 1024;
                    using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                } 

                var result = new
                {
                    message = "Upload successful" 
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }
        }

        #endregion
    }
}
