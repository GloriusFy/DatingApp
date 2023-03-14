using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace WebApi.CrossCutting.Auth
{
    public class AuthorizationHeaderFromCookiesMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationHeaderFromCookiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[IdentityConstant.AccessTokenCookieKey];

            if (!string.IsNullOrEmpty(token))
                context.Request.Headers.Add(HeaderNames.Authorization, JwtBearerDefaults.AuthenticationScheme + " " + token);

            await _next(context);
        }
    }
}
