using ESApplication;
using ESApplication.AggregateModels;
using ESApplication.Commands.BusinessImages;
using ESApplication.Models;
using ESApplication.Queries.GetBusinessImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessImagesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        private readonly IWebHostEnvironment _hostingEnvironment;        
        public BusinessImagesController(IMediator mediator, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _mediator = mediator;
            _config = config;
            _hostingEnvironment = hostingEnvironment;

        }

        #region Business Image Actions  
        /// <summary>
        /// To get Business Image details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<BusinessImagesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<BusinessImagesDto>>> GetBusinessImageDetails(string id)
        {
            var res = await _mediator.Send(new GetBusinessImagesDetailsQuery() { id = id });
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }        

        /// <summary>
        /// Upload Business Images
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UploadBusinessImages")]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UploadBusinessImages()
        {
            try
            {
                if (!Request.Form.Files.Any())
                    return BadRequest("No files found in the request");

                if (Request.Form.Files[0].Length <= 0)
                    return BadRequest("Invalid file length, seems to be empty");

                var formCollection = await Request.ReadFormAsync();

                var imageSrcPath = Utilities.GetImageSrcPath(formCollection, "BusinessImages");

                var imageNames = Utilities.UploadImages(formCollection.Files, imageSrcPath, _hostingEnvironment);

                if(imageNames.Count < 1) return StatusCode(StatusCodes.Status500InternalServerError, "Error");

                var request = new UploadBusinessImagesCommand() {
                    UserId = formCollection["userid"].ToString(),
                    BusinessId = formCollection["businessId"].ToString(),
                    type = Convert.ToInt16(formCollection["type"]),
                    ImagePath = imageSrcPath,
                    ImageNames= imageNames
                };               

                var reportTypeResponse = await _mediator.Send(request);
                return Ok(reportTypeResponse);
                
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To update Business Images
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("DeleteBusinessImage")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteBusinessImage([FromBody] DeleteBusinessImagesCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);

        }
        #endregion 
    }
}
