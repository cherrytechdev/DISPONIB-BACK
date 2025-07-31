using ESApplication.AggregateModels;
using ESApplication.Models;
using ESApplication.Queries.GetRequesterAction;
using ESDomain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveDirectoryController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ActiveDirectoryController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region Active Directory 
        /// <summary>
        /// To get Get Active Directory User
        /// </summary>   
        /// <returns></returns>

        [HttpGet("GetActiveDirectoryUser/{emailID}")]
        [ProducesResponseType(typeof(List<AdUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AdUser>>> GetActiveDirectoryUser(string emailID)
        {
            try
            {
                AdUser User = new AdUser();
                DirectoryEntry entry = new DirectoryEntry("LDAP://EISF");
                DirectorySearcher adSearcher = new DirectorySearcher(entry);
                adSearcher.SearchScope = SearchScope.Subtree;
                adSearcher.Filter = "(&(objectClass=user)(mail=" + emailID + "))";
                SearchResult userObject = adSearcher.FindOne();
                if (userObject != null)
                {
                    try
                    {
                        User.userid = userObject.Properties["samaccountname"][0].ToString();
                        User.username = userObject.Properties["displayname"][0].ToString();
                        User.email = userObject.Properties["mail"][0].ToString();
                        User.mobile= userObject.Properties["mail"][0].ToString();
                        var tokenString = GenerateJwtToken(User);
                        User.jwttoken = tokenString;
                        return Ok(User);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To get Get Active Directory All Users
        /// </summary>   
        /// <returns></returns>
        [HttpGet("GetActiveDirectoryAllUsers")]
        [ProducesResponseType(typeof(List<AdUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AdUser>>> GetActiveDirectoryAllUsers()
        {
            try
            {
                List<AdUser> lstADUsers = new List<AdUser>(); 
                DirectoryEntry entry = new DirectoryEntry("LDAP://EISF");
                DirectorySearcher adSearcher = new DirectorySearcher(entry); 
                adSearcher.SearchScope = SearchScope.Subtree; 
                adSearcher.Filter = "(&(objectClass=user))";
                adSearcher.PropertiesToLoad.Add("samaccountname");
                adSearcher.PropertiesToLoad.Add("mail"); 
                adSearcher.PropertiesToLoad.Add("displayname");
                SearchResult result;
                SearchResultCollection resultCol = adSearcher.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") &&
                                 result.Properties.Contains("mail") &&
                            result.Properties.Contains("displayname"))
                        {
                            AdUser objSurveyUsers = new AdUser();
                            objSurveyUsers.email = (String)result.Properties["mail"][0];
                            objSurveyUsers.mobile = (String)result.Properties["mail"][0];
                            objSurveyUsers.userid = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.username = (String)result.Properties["displayname"][0];
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return Problem("Data not found at Active Directory", null, StatusCodes.Status404NotFound);
            }
        }

        #endregion

        #region generate token
        string GenerateJwtToken(AdUser userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userInfo.username)
            };

            claims.Add(new Claim(ClaimTypes.Role, userInfo.email));

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
