using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Vidly.WebApi.Services.Users;

namespace Vidly.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizationFilterAttribute(string? permission = null)
        : Attribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Result != null)
            {
                return;
            }

            var userLogged = context.HttpContext.Items[Item.UserLogged];

            var userIsNotIdentified = userLogged == null;
            if (userIsNotIdentified)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "UnAuthorized",
                    Message = $"Not authenticated"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }
            var userLoggedMapped = (User)userLogged;

            var permission = BuildPermission(context);

            var hasNotPermission = !userLoggedMapped.HasPermission(permission);

            if (hasNotPermission)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "Forbidden",
                    Message = $"Missing permission {permission}"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }

        private string BuildPermission(AuthorizationFilterContext context)
        {
            return permission ?? $"{context.RouteData.Values["action"]}-{context.RouteData.Values["controller"]}";
        }
    }
}
