using Newtonsoft.Json; // أو System.Text.Json
using System.Net;
namespace PersonalityAssessment.Api.Middleware
{


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    message = ex.Message,
                    statusCode = context.Response.StatusCode
                };


                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
