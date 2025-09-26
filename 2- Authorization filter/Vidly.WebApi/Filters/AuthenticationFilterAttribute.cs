using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Net;
using Vidly.WebApi.Services.Sessions;
using Vidly.WebApi.Services.Users;

namespace Vidly.WebApi.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthenticationFilterAttribute
        : Attribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            // 1. Validar que venga un valor en el header Authorization
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "Unauthenticated",
                    Message = "You are not authenticated"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // 2. Validar que el valor en el header Authorization tenga formato correcto.
            // Por ejemplo, si generamos tokens como GUID, debemos validar que el valor sea un GUID
            // valido y no cualquier otro valor
            var isAuthorizationFormatNotValid = !IsAuthorizationFormatValid(authorizationHeader);
            if (isAuthorizationFormatNotValid)
            {
                context.Result = new ObjectResult(
                    new
                    {
                        InnerCode = "InvalidAuthorization",
                        Message = "The provided authorization header format is invalid"
                    })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // 3. Validar que el token no sea uno expirado/deprecado, sea uno vigente
            var isAuthorizationExpired = IsAuthorizationExpired(authorizationHeader);
            if (isAuthorizationExpired)
            {
                context.Result = new ObjectResult(
                    new
                    {
                        InnerCode = "ExpiredAuthorization",
                        Message = "The provided authorization header is expired"
                    })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            try
            {
                var userOfAuthorization = GetUserOfAuthorization(authorizationHeader, context);

                context.HttpContext.Items[Item.UserLogged] = userOfAuthorization;
            }
            catch (Exception)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "InternalError",
                    Message = "An error ocurred while processing the request"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        private bool IsAuthorizationFormatValid(string authorization)
        {
            return true;
        }

        private bool IsAuthorizationExpired(string authorization)
        {
            return false;
        }

        private User GetUserOfAuthorization(
            string authorization,
            AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

            var user = sessionService.GetUserByToken(authorization);

            return user;
        }
    }
}
