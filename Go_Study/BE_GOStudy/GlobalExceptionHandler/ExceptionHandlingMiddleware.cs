using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BE_GOStudy.GlobalExceptionHandler
{
     
        public class ExceptionHandlingMiddleware : IMiddleware
        {
            private readonly ILogger<ExceptionHandlingMiddleware> _logger;

            public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
            {
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                try
                {
                    await next(context);
                }
                catch (ResourceNotFoundException ex)
                {
                    _logger.LogWarning(ex, ex.Message);
                    await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Resource not found", ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Server error", ex.Message);
                }
            }

            private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
            {
                context.Response.StatusCode = (int)statusCode;

                var problem = new ProblemDetails
                {
                    Status = (int)statusCode,
                    Title = title,
                    Detail = detail
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
        public class ResourceNotFoundException : Exception
        {
            public ResourceNotFoundException() : base("The requested resource could not be found.") { }

            public ResourceNotFoundException(string message) : base(message) { }

            public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        }
    }
 
