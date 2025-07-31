using ESApplication.AggregateModels;
using ESApplication.Commands.Promotion;
using ESApplication.Models;
using ESApplication.Queries.GetPromotion;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public PromotionController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config; 

        }

        #region Promotion Data  

        /// <summary>
        /// To get Promotion details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(List<PromotionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PromotionDto>>> GetPromotionDetails(string userid)
        {
            var res = await _mediator.Send(new GetPromotionDetailsQuery() { userid = userid });
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
        /// To create Promotion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update Promotion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotionCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update Promotion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deletepromotion")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeletePromotion([FromBody] DeletePromotionCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 
    }  
}
