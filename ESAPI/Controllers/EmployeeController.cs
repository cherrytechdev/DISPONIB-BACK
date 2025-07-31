using ESApplication.AggregateModels;
using ESApplication.Commands.UserDetails;
using ESApplication.Queries.GetLogin;
using ESApplication.Queries.GetMasterSelectDetails;
using ESApplication.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ESApplication.Models;
using ESApplication.Commands.EmployeeDetails;
using ESApplication.Queries.GetEmployeeDetails;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public EmployeeController(IMediator mediator, IConfiguration config, IOptions<MailSettings> oDataurls)
        {
            _mediator = mediator;
            _config = config;
            _oDataurls = oDataurls.Value;

        }


        #region employee 

        /// <summary>
        /// To get Employee Details.
        /// </summary>  
        ///   /// <param name="type">type</param>
        /// <returns></returns>
        [HttpGet("getemployee")]
        [ProducesResponseType(typeof(List<EmployeeDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeeDetailsDto>>> GetMasterSelectDetails()
        {
            var res = await _mediator.Send(new GetEmployeeQuery());
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }
        #endregion

        #region User Details  

         

        /// <summary>
        /// To create Employee Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserDetails(CreateEmployeeCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion 

         
    }
     
}
