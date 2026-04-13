using PersonalityAssessment.Core.Admin;

namespace PersonalityAssessment.Core.Interface
{
    public interface IAdminDataService
    {
        Task<AdminDashboardOverviewData> GetDashboardOverviewAsync(CancellationToken cancellationToken = default);

        Task<AdminPagedUsersData> GetUsersPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

        Task<AdminUserDetailData?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);

        Task<bool> TryDeleteUserAsync(string userId, CancellationToken cancellationToken = default);

        Task<List<AdminAssessmentListItemData>> GetAssessmentsAsync(CancellationToken cancellationToken = default);

        Task<AdminAssessmentDetailData?> GetAssessmentByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> TryDeleteAssessmentAsync(int id, CancellationToken cancellationToken = default);

        Task<AdminAnalyticsData> GetAnalyticsAsync(int trendDays, CancellationToken cancellationToken = default);

        Task<AdminDatabaseHealthData> GetDatabaseHealthAsync(CancellationToken cancellationToken = default);
    }
}
