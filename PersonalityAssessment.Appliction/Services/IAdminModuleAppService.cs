using PersonalityAssessment.Application.Features.Admin.DTO;

namespace PersonalityAssessment.Application.Services
{
    public interface IAdminModuleAppService
    {
        Task<AdminDashboardOverviewDto> GetDashboardAsync(CancellationToken cancellationToken = default);

        Task<AdminPagedUsersDto> GetUsersAsync(int page, int pageSize, CancellationToken cancellationToken = default);

        Task<AdminUserDetailDto?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken = default);

        Task<List<AdminAssessmentListItemDto>> GetAssessmentsAsync(CancellationToken cancellationToken = default);

        Task<AdminAssessmentListItemDto?> GetAssessmentByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> DeleteAssessmentAsync(int id, CancellationToken cancellationToken = default);

        Task<AdminAnalyticsDto> GetAnalyticsAsync(int trendDays, CancellationToken cancellationToken = default);

        Task<AdminSystemHealthDto> GetSystemHealthAsync(CancellationToken cancellationToken = default);
    }
}
