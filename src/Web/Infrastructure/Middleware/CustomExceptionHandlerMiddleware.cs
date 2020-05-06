using System;
using System.Net;
using System.Threading.Tasks;
using EGID.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EGID.Web.Infrastructure.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case EntityNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            if (result == string.Empty) 
                result = JsonConvert.SerializeObject(new {error = exception.Message});

            return context.Response.WriteAsync(result);
        }
    }
}
