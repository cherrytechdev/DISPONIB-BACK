using ESApplication.AggregateModels;
using ESApplication.Commands.Business;
using ESApplication.Commands.CommonData;
using ESApplication.Commands.Promotion;
using ESApplication.Models;
using ESApplication.Queries.GetBusiness;
using ESApplication.Queries.GetCommonDetails;
using ESApplication.Queries.GetPromotion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public CommonController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;

        }

        #region Common Data  

        /// <summary>
        /// To get Reports details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("reports/{code1}/{code2}/{startdate}/{enddate}")]
        [ProducesResponseType(typeof(List<ReportsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ReportsDto>>> GetReportsDetails(string code1, string code2, string startdate, string enddate)
        {
            var res = await _mediator.Send(new GetReportsDetailsQuery()
            {
                code1 = code1,
                code2 = code2,
                startdate = startdate,
                enddate = enddate
            });
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
        /// To get Common details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}/{type}")]
        [ProducesResponseType(typeof(List<CommonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CommonDto>>> GetCommonDetails(string userid, int type)
        {
            var res = await _mediator.Send(new GetCommonDetailsQuery() { userid = userid, type = type });
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
        /// To create Common
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCommon([FromBody] CreateCommonCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }
        #endregion
    }
}
