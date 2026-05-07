using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Api.Services
{
    public sealed class AdminRequestMetricsService : IAdminRequestMetrics
    {
        private long _total;

        public void IncrementRequestCount()
        {
            Interlocked.Increment(ref _total);
        }

        public long TotalHttpRequests => Interlocked.Read(ref _total);
    }
}
