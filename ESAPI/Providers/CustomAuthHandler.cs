using ESApplication.Queries.GetTokenDetails;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ESAPI.Providers
{
    public class CustomAuthHandler
    {
        public bool IsValidToken(string jwtToken, string issuer, string audience, string metadataAddress)
        {
            //var openIdConnectConfig = AuthConfigManager.GetMetaData(metadataAddress);
            //var signingKeys = openIdConnectConfig.SigningKeys;
            //return ValidateToken(jwtToken, issuer, audience, signingKeys);
            return ValidateToken(jwtToken, issuer, audience, null);
        }
        private bool ValidateToken(string jwtToken, string issuer, string audience, ICollection<SecurityKey> signingKeys)
        {
            try
            {
                GetTokenDetailsQuery dbToken = new GetTokenDetailsQuery() { token= jwtToken };
                if(dbToken.token==null) throw new Exception("404 - Authorization failed - Invalid Token");
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = signingKeys,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KqcL7s998JrfFHRP")),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience
                };;
                ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
                var claim = tokenValidator.ValidateToken(jwtToken, validationParameters, out var validatedToken);
                var dbClaim = tokenValidator.ValidateToken(dbToken.token, validationParameters, out var _);
                var userId = dbClaim.FindFirst(c => c.Type.ToLower() == "userid")?.Value;
                //tokenValidator.ValidateToken(jwtToken, validationParameters, out
                //    var _);
                var scope = claim.FindFirst(c => c.Type.ToLower() == "userid" && (c.Value == userId));
                if (scope == null) throw new Exception("404 - Authorization failed - Invalid Scope");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("404 - Authorization failed", ex);
            }
        }


    }
}
