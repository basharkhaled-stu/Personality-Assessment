namespace PersonalityAssessment.Core.Interface
{
    public interface IAdminRequestMetrics
    {
        void IncrementRequestCount();
        long TotalHttpRequests { get; }
    }
}
