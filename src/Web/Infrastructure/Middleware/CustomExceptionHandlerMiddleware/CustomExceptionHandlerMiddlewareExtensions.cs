using Microsoft.AspNetCore.Builder;

namespace EGID.Web.Infrastructure.Middleware.CustomExceptionHandlerMiddleware
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EGID.Web.Infrastructure.Middleware.CustomExceptionHandlerMiddleware.CustomExceptionHandlerMiddleware>();
        }
    }
}