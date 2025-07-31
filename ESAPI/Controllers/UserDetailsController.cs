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
using ESApplication.Queries.GetReviewDetails;
using ESApplication.Queries.GetDashboard;
using Microsoft.AspNetCore.Authorization;
using ESApplication.Commands.TokenDetails;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDetailsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly MailSettings _oDataurls;
        public UserDetailsController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;

        }

        #region Login   
        /// <summary>
        /// To get login details.
        /// </summary>  
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDto>> login(LoginDetails loginDetails)
        {
            var res = await _mediator.Send(new GetLoginQuery() { username = loginDetails.username, password = loginDetails.password });
            if (!String.IsNullOrEmpty(res[0].userid))
            {
                var tokenString = GenerateJwtToken(res);
                AuthDto authDto = new AuthDto();
                authDto.jwttoken = tokenString;
                
                var reportTypeResponse = await _mediator.Send(new CreateTokenCommand() {token= tokenString });
                //HttpContext.Session.SetString("jwttoken", tokenString);
                return Ok(authDto);
            }
            else
            {
                return Problem("Unauthorized User! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }

        #endregion

        #region company

        /// <summary>
        /// To get Master Select Details.
        /// </summary>  
        ///   /// <param name="type">type</param>
        /// <returns></returns>
        [HttpGet("masterselectdetails/{type}/{userid}/{val1}")]
        [ProducesResponseType(typeof(List<MasterSelectDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MasterSelectDetailsDto>>> GetMasterSelectDetails(int type, string userid, string val1)
        {
            var res = await _mediator.Send(new GetMasterSelectDetailsQuery() { type = type, userid = userid, val1 = val1 });
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
        /// To get user review details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        //[HttpGet("getreviewdetails/{businessid}/{isactive}/{isquiz}")]
        //[ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[AllowAnonymous]
        //public async Task<ActionResult<List<UserDetailsDto>>> GetReviewDetails(string businessid, Int16 isactive, Int16 isquiz)
        //{
        //    var res = await _mediator.Send(new GetReviewDetailsQuery() { businessid = businessid, isactive = isactive, isquiz = isquiz });
        //    if (res != null)
        //    {
        //        return Ok(res);
        //    }
        //    else
        //    {
        //        return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
        //    }
        //}

        [HttpGet("getreviewdetails/{userid}/{businessid}/{isactive}/{isquiz}")]
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<UserDetailsDto>>> GetReviewDetails(string userid, string businessid, Int16 isactive, Int16 isquiz)
        {
            var res = await _mediator.Send(new GetReviewDetailsQuery() { userid = userid, businessid = businessid, isactive = isactive, isquiz = isquiz });
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
        /// To get user details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDetailsDto>>> GetUserDetails(string userid)
        {
            var res = await _mediator.Send(new GetUserDetailsQuery() { userid = userid });
            if (res != null)
            {
                var tokenString = GenerateJwtToken(res);
                //response.jwttoken = tokenString;
                return Ok(res);
            }
            else
            {
                return Problem("Records not found! Please contact the administrator.", null, StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// To get Dashboard details.
        /// </summary> 
        /// /// <param name="userid">userid</param>
        /// /// <param name="businessid">businessid</param>
        /// <returns></returns>
        [HttpGet("getdashboarddata/{userid}/{businessid}")]
        [ProducesResponseType(typeof(List<DashboardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DashboardDto>>> GetDashboardDetails(string userid, string businessid)
        {
            var res = await _mediator.Send(new GetDashboardDetailsQuery() { userid = userid, businessid = businessid });
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserDetails(CreateUserDetailsCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        /// <summary>
        /// To update UserDetails
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }


        /// <summary>
        /// To update UserDetails
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("deleteuserdetails")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteUserDetails([FromBody] DeleteUserDetailsCommand request)
        {
            var reportTypeResponse = await _mediator.Send(request);
            return Ok(reportTypeResponse);
        }

        #endregion

        #region generate token
        string GenerateJwtToken(List<UserDetailsDto> userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo[0].email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userInfo[0].username)
            };

            claims.Add(new Claim(ClaimTypes.Role, userInfo[0].roleid));
            claims.Add(new Claim("fullname", userInfo[0].firstname + " " + userInfo[0].lastname));
            claims.Add(new Claim("email", userInfo[0].email));
            claims.Add(new Claim("mobile", userInfo[0].mobile));
            claims.Add(new Claim("username", userInfo[0].username));
            claims.Add(new Claim("userid", userInfo[0].userid));
            claims.Add(new Claim("role", userInfo[0].roleid));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, Startup.Configuration["Jwt:Provider"]));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiryMinutes = Int32.Parse(Startup.Configuration["Jwt:ExpiryMinutes"]);
            var expires = DateTime.Now.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                Startup.Configuration["Jwt:Issuer"],
                Startup.Configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
