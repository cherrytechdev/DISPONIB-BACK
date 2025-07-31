using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ESAPI.Auth
{
    public class UserIdRequirement : IAuthorizationRequirement { }
    public class UserIdAuthorizationHandler : AuthorizationHandler<UserIdRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIdRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst("userid")?.Value;
            var httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;
            var requestedUserId = httpContext?.Request.RouteValues["userid"]?.ToString();

            if (userIdClaim == requestedUserId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
