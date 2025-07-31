using ESApplication.AggregateModels;
using ESApplication.Commands.Business;
using ESApplication.Commands.BusinessHours;
using ESApplication.Commands.Promotion;
using ESApplication.Models;
using ESApplication.Queries.GetBusiness;
using ESApplication.Queries.GetBusinessHours;
using ESApplication.Queries.GetPromotion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessHoursController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public BusinessHoursController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;

        }

        #region Business Hours Data  

        /// <summary>
        /// To get Business Hours details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<BusinessDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<BusinessHoursDto>>> GetBusinessHoursDetails(string id)
        {
            var res = await _mediator.Send(new GetBusinessHoursDetailsQuery() { id = id });
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
        /// To create Business Hours
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBusinessHours([FromBody] CreateBusinessHoursCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update Business Hours
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateBusinessHours([FromBody] UpdateBusinessHoursCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update Business Hours
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deletebusinesshours")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteBusinessHours([FromBody] DeleteBusinessHoursCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 
    }
}
