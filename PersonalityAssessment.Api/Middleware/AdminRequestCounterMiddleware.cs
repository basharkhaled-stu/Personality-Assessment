using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Api.Middleware
{
    public sealed class AdminRequestCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminRequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAdminRequestMetrics metrics)
        {
            metrics.IncrementRequestCount();
            await _next(context);
        }
    }
}
