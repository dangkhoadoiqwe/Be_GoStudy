using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BE_GOStudy.GlobalExceptionHandler
{
    public class ArgumentExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ArgumentExceptionHandlingMiddleware> _logger;
        public ArgumentExceptionHandlingMiddleware(ILogger<ArgumentExceptionHandlingMiddleware> logger)
            => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ProblemDetails propblem = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Sever error",
                    Title = "Bad Request",
                    Detail = e.Message
                };
                string json = JsonSerializer.Serialize(propblem);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}
