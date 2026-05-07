using PersonalityAssessment.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace PersonalityAssessment.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // BUG FIX: map exception types to correct HTTP status codes
            var statusCode = ex switch
            {
                NotFoundException       => HttpStatusCode.NotFound,           // 404
                BadRequestException     => HttpStatusCode.BadRequest,         // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,   // 401
                FluentValidation.ValidationException => HttpStatusCode.UnprocessableEntity, // 422
                _                       => HttpStatusCode.InternalServerError // 500
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = (int)statusCode;

            var response = new
            {
                message    = ex.Message,
                statusCode = context.Response.StatusCode,
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
