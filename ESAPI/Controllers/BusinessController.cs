using ESApplication.AggregateModels;
using ESApplication.Commands.Business;
using ESApplication.Commands.Promotion;
using ESApplication.Models;
using ESApplication.Queries.GetBusiness;
using ESApplication.Queries.GetPromotion;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public BusinessController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config; 

        }

        #region Business Data  

        /// <summary>
        /// To get Business details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}/{id}")]
        [ProducesResponseType(typeof(List<BusinessDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BusinessDto>>> GetBusinessDetails(string userid,string id)
        {
            var res = await _mediator.Send(new GetBusinessDetailsQuery() { userid = userid,id=id });
            if (res != null)
            { 
                return Ok(res); 
            }
            else
            {
                return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }
/**
        /// <summary>
        /// To get all Business records.
        /// </summary>
        /// <returns>List of BusinessDto</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BusinessDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BusinessDto>>> GetAllBusiness()
        {
            var res = await _mediator.Send(new GetAllBusinessQuery());
            if (res != null && res.Any())
            {
               return Ok(res);
            }
            else
            {
               return Problem("No business records found.", null, StatusCodes.Status404NotFound);
            }
        }**/


        /// <summary>
        /// To create Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateBusiness([FromBody] UpdateBusinessCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deletebusiness")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteBusiness([FromBody] DeleteBusinessCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 
    }  
}
