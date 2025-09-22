using System.Net;
using System.Security.Authentication;

namespace Glossary.API.Middleware;

public class ExceptionMappingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMappingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = ex switch
            {
                AuthenticationException => (int)HttpStatusCode.Unauthorized, 
                UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                KeyNotFoundException => (int)HttpStatusCode.NotFound, 
                _ => (int)HttpStatusCode.InternalServerError 
            };

            await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
    }
}
