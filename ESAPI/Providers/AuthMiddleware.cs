//using ESAPI.IRepositories;
//using ESApplication.Queries.GetTokenDetails;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace ESAPI.Providers
//{
//    public class AuthMiddleware: IAuthMiddleware
//    {
//        private readonly RequestDelegate next;
//        private readonly IConfiguration configuration;
//        public AuthMiddleware(IConfiguration appConfigurationr, RequestDelegate _next)
//        {
//            configuration = appConfigurationr;
//            next = _next;
//        }
//        public async Task Invoke(HttpContext httpContext)
//        {
//            try
//            {


//                var pathsToSkip = new[] { "/userdetails/login", "/api/public/public-other" };

//                if (!pathsToSkip.Contains(httpContext.Request.Path.Value?.ToLower()))
//                {
//                    var user = httpContext.User;
//                    var requestedUserId = httpContext?.Request.RouteValues["userid"]?.ToString();
//                    //var sessionToken = httpContext.Session.GetString("jwttoken");
//                    //if (string.IsNullOrEmpty(sessionToken))
//                    //{
//                    //     throw new Exception("Authorization header is empty");
//                    //}
//                    //var path = httpContext.Request.Path;
//                    string token = string.Empty;
//                    string issuer = "https://localhost:44359/"; //Get issuer value from your configuration
//                    string audience = "https://localhost:44359/"; //Get audience value from your configuration
//                    string metaDataAddress = issuer + "/.well-known/oauth-authorization-server";
//                    //CustomAuthHandler authHandler = new CustomAuthHandler();

//                    var header = httpContext.Request.Headers["Authorization"];
//                    if (header.Count == 0) throw new Exception("Authorization header is empty");
//                    string[] tokenValue = Convert.ToString(header).Trim().Split(" ");
//                    if (tokenValue.Length > 1)
//                    {
//                        token = tokenValue[1];
//                    }

//                    else throw new Exception("Authorization token is empty");
//                    if (IsValidToken(token, issuer, audience, metaDataAddress, requestedUserId)) await next(httpContext);
//                }
//                else
//                {
//                    await next(httpContext);
//                }
//            }
//            catch (Exception)
//            {
//                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                HttpResponseWritingExtensions.WriteAsync(httpContext.Response, "{\"message\": \"Unauthorized\"}").Wait();
//            }
//        }

//        public bool IsValidToken(string jwtToken, string issuer, string audience, string metadataAddress,string requestedUserId)
//        {
//            //var openIdConnectConfig = AuthConfigManager.GetMetaData(metadataAddress);
//            //var signingKeys = openIdConnectConfig.SigningKeys;
//            //return ValidateToken(jwtToken, issuer, audience, signingKeys);
//            return ValidateToken(jwtToken, issuer, audience, null, requestedUserId);
//        }
//        private bool ValidateToken(string jwtToken, string issuer, string audience, ICollection<SecurityKey> signingKeys,string requestedUserId)
//        {
//            try
//            {
//                GetTokenDetailsQuery dbToken = new GetTokenDetailsQuery() { token = jwtToken };
//                if (dbToken.token == null) throw new Exception("404 - Authorization failed - Invalid Token");
//                var validationParameters = new TokenValidationParameters
//                {
//                    RequireExpirationTime = true,
//                    ValidateLifetime = true,
//                    ClockSkew = TimeSpan.FromMinutes(1),
//                    RequireSignedTokens = true,
//                    ValidateIssuerSigningKey = true,
//                    //IssuerSigningKey = signingKeys,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KqcL7s998JrfFHRP")),
//                    ValidateIssuer = true,
//                    ValidIssuer = issuer,
//                    ValidateAudience = true,
//                    ValidAudience = audience
//                }; ;
//                ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
//                var claim = tokenValidator.ValidateToken(jwtToken, validationParameters, out var validatedToken);
//                var dbClaim = tokenValidator.ValidateToken(dbToken.token, validationParameters, out var _);
//                var tokenUserId = claim.FindFirst(c => c.Type.ToLower() == "userid")?.Value;
//                var dbUserId = dbClaim.FindFirst(c => c.Type.ToLower() == "userid")?.Value;
//                //tokenValidator.ValidateToken(jwtToken, validationParameters, out
//                //    var _);

//                var handler = new JwtSecurityTokenHandler();
//                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

//                var dbTokens = handler.ReadToken(dbToken.token) as JwtSecurityToken;
//                //var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
//                var jsonTokenusername = jsonToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
//                var dbTokenusername = jsonToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
//                var jsonExpirationTime = jsonToken?.ValidTo;
//                var dbTokensExpirationTime = dbTokens?.ValidTo;

//                if (jsonExpirationTime < DateTime.UtcNow || dbTokensExpirationTime < DateTime.UtcNow)
//                {
//                    throw new SecurityTokenException("Token has expired.");
//                }
//                if (jsonTokenusername != dbTokenusername)
//                {
//                    throw new Exception("Invalid User");
//                }

//                if (requestedUserId != null && (requestedUserId != dbUserId && requestedUserId != tokenUserId))
//                {
//                    throw new Exception("401 - Access Denied");
//                }
//                var scope = claim.FindFirst(c => c.Type.ToLower() == "userid" && (c.Value == dbUserId));
//                if (scope == null) throw new Exception("404 - Authorization failed - Invalid Scope");
//                return true;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("404 - Authorization failed", ex);
//            }
//        }
//    }

//}

using ESAPI.IRepositories;
using ESApplication.Queries.GetTokenDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ESAPI.Providers
{
    public class AuthMiddleware : IAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
 
        public AuthMiddleware(IConfiguration configuration, RequestDelegate next)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var pathsToSkip = new[] { "/userdetails/login", "/sitedata/" };
                var endpoint = httpContext.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
                {
                    // If the endpoint is marked as [AllowAnonymous], skip authentication.
                    await _next(httpContext);
                    return;
                }
                // Skip specific paths
                if (!pathsToSkip.Contains(httpContext.Request.Path.Value?.ToLower()))
                {
                    var requestedUserId = httpContext?.Request.RouteValues["userid"]?.ToString();
                    if (requestedUserId != null && "empty".Equals(requestedUserId))
                    {
                        await _next(httpContext);
                        return;
                    }
                    var token = extractTokenFromHeader(httpContext);

                    if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Authorization token is empty");

                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var metaDataAddress = $"{issuer}/.well-known/oauth-authorization-server"; // Optionally use this if needed for OpenID Connect

                    // Validate the token
                    if (isValidToken(token, issuer, audience, metaDataAddress, requestedUserId))
                    {
                        await _next(httpContext);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Invalid token");
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync($"{{\"message\": \"Unauthorized\", \"error\": \"{ex.Message}\"}}");
            }
        }

        private string extractTokenFromHeader(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(header)) return null;

            var tokenValue = header.Split(" ");
            return tokenValue.Length > 1 ? tokenValue[1] : null;
        }

        public bool isValidToken(string jwtToken, string issuer, string audience, string metadataAddress, string requestedUserId)
        {
            // First validate the JWT token
            if (!validateToken(jwtToken, issuer, audience, requestedUserId))
            {
                return false;
            }

            // Then, verify if the token exists in the database
            if (!verifyTokenFromDb(jwtToken))
            {
                return false;
            }

            return true;
        }

        private bool validateToken(string jwtToken, string issuer, string audience, string requestedUserId)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience
                };

                var handler = new JwtSecurityTokenHandler();
                var claim = handler.ValidateToken(jwtToken, validationParameters, out var validatedToken);

                // Extract user IDs from token claims
                var tokenUserId = claim.FindFirst(c => c.Type.ToLower() == "userid")?.Value;

                if (string.IsNullOrEmpty(tokenUserId)) throw new UnauthorizedAccessException("User ID missing in token.");

                if (requestedUserId != null && requestedUserId != tokenUserId)
                {
                    throw new UnauthorizedAccessException("Access Denied. User ID mismatch.");
                }

                return true;

            }
            catch (SecurityTokenException ex)
            {
                throw new UnauthorizedAccessException("Token validation failed.", ex);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Authorization failed", ex);
            }
        }

        private bool verifyTokenFromDb(string jwtToken)
        {
            try
            {
                GetTokenDetailsQuery getTokenDetails = new GetTokenDetailsQuery{token= jwtToken };// .GetTokenByUserId(requestedUserId);
                var handler = new JwtSecurityTokenHandler();
                var dbToken = handler.ReadToken(getTokenDetails.token) as JwtSecurityToken;
                var dbTokensExpirationTime = dbToken?.ValidTo;
                if (getTokenDetails == null) throw new UnauthorizedAccessException("Token not found in database.");
                if (getTokenDetails.token != jwtToken) throw new UnauthorizedAccessException("Token mismatch in database.");

                // Optionally, you can verify additional properties like expiration or token status
                if (dbTokensExpirationTime < DateTime.UtcNow) throw new UnauthorizedAccessException("Token has expired.");

                return true;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Error verifying token from database.", ex);
            }
        }
    }
}

