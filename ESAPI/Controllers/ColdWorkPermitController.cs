using ESApplication.AggregateModels;
using ESApplication.Commands.ColdWorkPermitDetails;
using ESApplication.Commands.UpdateRequesterAction;
using ESApplication.Commands.UpdateRequesterActionDetails;
using ESApplication.EmailServices;
using ESApplication.Queries.GetApprovalsDetails;
using ESApplication.Queries.GetColdWorkPermitDetails;
using ESApplication.Queries.GetDocumentsDetails;
using ESApplication.Queries.GetRequesterAction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class ColdWorkPermitController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMailService mailService;

        public ColdWorkPermitController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            this.mailService = mailService;
        }

        #region Cold Work Permit 

        /// <summary>
        /// To get document details.
        /// </summary>  
        /// <param name="referenceid">referenceid</param> 
        /// <returns></returns>
        [HttpGet("GetRequesterAction/{referenceid}")]
        [ProducesResponseType(typeof(List<RequesterActionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RequesterActionDto>>> GetRequesterAction(string referenceid)
        {
            var res = await _mediator.Send(new GetRequesterActionQuery() { referenceid = referenceid });
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Result not found", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To get document details.
        /// </summary>  
        /// <param name="referenceid">referenceid</param> 
        /// <returns></returns>
        [HttpGet("GetDocumentsDetails/{referenceid}")]
        [ProducesResponseType(typeof(List<DocumentsDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DocumentsDetailsDto>>> GetDocumentsDetailsQuery(string referenceid)
        {
            var res = await _mediator.Send(new GetDocumentsDetailsQuery() { referenceid = referenceid });
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Result not found", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To get Get ColdWorkPermit Details.
        /// </summary>  
        /// <param name="userid">userid</param> 
        /// /// <param name="type">type</param> 
        /// <returns></returns>
        [HttpGet("{userid}/{type}")]
        [ProducesResponseType(typeof(List<ColdWorkPermitDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ColdWorkPermitDetailDto>>> GetColdWorkPermitDetails(string userid, int type)
        {
            var res = await _mediator.Send(new GetColdWorkPermitDetailsQuery() { userid = userid, type = type });
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Result not found", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To get Get Approvals Details.
        /// </summary>  
        /// <param name="userid">userid</param> 
        /// <param name="permittype">permittype</param> 
        /// <returns></returns>
        [HttpGet("approvalshistory/{referenceid}/{permittype}")]
        [ProducesResponseType(typeof(List<ApprovalsDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ApprovalsDetailsDto>>> GetApprovalsDetails(string referenceid, int permittype)
        {
            var res = await _mediator.Send(new GetApprovalsDetailsQuery() { referenceid = referenceid, permittype = permittype });
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Result not found", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To create ColdWorkPermit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        //public async Task<IActionResult> CreateColdWorkPermitDetails([FromForm] CreateColdWorkPermitCommand request )
        //{ 
        //    var reportTypeResponse = await _mediator.Send(request); 
        //    await mailService.SendEmailAsync();
        //    return Ok(reportTypeResponse);
        //}

        /// <summary>
        /// To update ColdWorkPermit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        //public async Task<IActionResult> UpdateColdWorkPermitDetails([FromForm] UpdateColdWorkPermitCommand request)
        //{
        //    var reportTypeResponse = await _mediator.Send(request);
        //    await mailService.SendEmailAsync();
        //    return Ok(reportTypeResponse);
        //} 

        /// <summary>
        /// To update ColdWorkPermit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("requesteraction")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> RequesterAction([FromBody] RequesterActionCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            await mailService.SendEmailAsync();
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update ColdWorkPermit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("updaterequesteraction")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateRequesterAction([FromBody] UpdateRequesterActionCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            await mailService.SendEmailAsync();
            return Ok(reportTypeResponse);
        }
         
        #endregion
    }
}
