using ESApplication;
using ESApplication.AggregateModels;
using ESApplication.Commands.SiteData;
using ESApplication.Models;
using ESApplication.Queries.GetSiteDataDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteDataController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SiteDataController(IMediator mediator, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _mediator = mediator;
            _config = config;
            _hostingEnvironment = hostingEnvironment;

        }

        #region Site Data  

        /// <summary>
        /// To get Site Data details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(List<SiteDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<SiteDataDto>>> GetSiteDataDetails(string userid)
        {
            var res = await _mediator.Send(new GetSiteDataDetailsQuery() { userid = userid });
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
        /// To create UserDetails
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateSiteData")]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSiteData()
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    if (!Request.Form.Files.Any())
                        return BadRequest("No files found in the request");

                    if (Request.Form.Files[0].Length <= 0)
                        return BadRequest("Invalid file length, seems to be empty");
                }

                var formCollection = await Request.ReadFormAsync();

                var imageSrcPath = Request.Form.Files.Count > 0 ? Utilities.GetImageSrcPath(formCollection, "SiteImagesImages") : "";

                var imageNames = Request.Form.Files.Count > 0 ? Utilities.UploadImages(formCollection.Files, imageSrcPath, _hostingEnvironment) : null;

                if (imageNames != null)
                    if (imageNames.Count <= 0) return StatusCode(StatusCodes.Status500InternalServerError, "Error");

                var request = new CreateSiteDataCommand()
                {
                    userid =formCollection["userid"].ToString(),
                    type = Convert.ToString(formCollection["type"]),
                    description = Convert.ToString(formCollection["description"]),
                    filePath = imageSrcPath,
                    fileNames = imageNames
                };

                var reportTypeResponse = await _mediator.Send(request);
                return Ok(reportTypeResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To update SiteData
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateSiteData([FromBody] UpdateSiteDataCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update SiteData
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deletesitedata")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteSiteData([FromBody] DeleteSiteDataCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 
    }
}
