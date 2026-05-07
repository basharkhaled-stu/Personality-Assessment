using PersonalityAssessment.Application.Features.Admin.DTO;
using PersonalityAssessment.Core.Interface;
using System.Diagnostics;

namespace PersonalityAssessment.Application.Services
{
    public class AdminModuleAppService : IAdminModuleAppService
    {
        private readonly IAdminDataService _adminData;
        private readonly IAdminRequestMetrics _requestMetrics;

        public AdminModuleAppService(IAdminDataService adminData, IAdminRequestMetrics requestMetrics)
        {
            _adminData = adminData;
            _requestMetrics = requestMetrics;
        }

        public async Task<AdminDashboardOverviewDto> GetDashboardAsync(CancellationToken cancellationToken = default)
        {
            var d = await _adminData.GetDashboardOverviewAsync(cancellationToken);
            return new AdminDashboardOverviewDto
            {
                TotalUsers = d.TotalUsers,
                TotalAssessments = d.TotalAssessments,
                TotalCompletedAssessments = d.TotalCompletedAssessments,
                TotalPendingAssessments = d.TotalPendingAssessments,
                TotalQuestions = d.TotalQuestions,
                TotalPersonalityTypes = d.TotalPersonalityTypes,
                TotalAnswersSubmitted = d.TotalAnswersSubmitted,
                MostCommonPersonalityTypeName = d.MostCommonPersonalityTypeName,
                MostCommonPersonalityTypeCount = d.MostCommonPersonalityTypeCount,
                AverageAssessmentScore = d.AverageAssessmentScore,
            };
        }

        public async Task<AdminPagedUsersDto> GetUsersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var p = await _adminData.GetUsersPagedAsync(page, pageSize, cancellationToken);
            return new AdminPagedUsersDto
            {
                TotalCount = p.TotalCount,
                Page = p.Page,
                PageSize = p.PageSize,
                Items = p.Items.Select(u => new AdminUserSummaryDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    EmailConfirmed = u.EmailConfirmed,
                    IsGoogleAccount = u.IsGoogleAccount,
                }).ToList(),
            };
        }

        public async Task<AdminUserDetailDto?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var u = await _adminData.GetUserByIdAsync(id, cancellationToken);
            if (u == null)
                return null;
            return new AdminUserDetailDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EmailConfirmed = u.EmailConfirmed,
                IsGoogleAccount = u.IsGoogleAccount,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                TwoFactorEnabled = u.TwoFactorEnabled,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd = u.LockoutEnd,
                Roles = u.Roles,
                CreatedAtApprox = u.CreatedAtApprox,
            };
        }

        public Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken = default)
            => _adminData.TryDeleteUserAsync(id, cancellationToken);

        public async Task<List<AdminAssessmentListItemDto>> GetAssessmentsAsync(CancellationToken cancellationToken = default)
        {
            var list = await _adminData.GetAssessmentsAsync(cancellationToken);
            return list.Select(a => new AdminAssessmentListItemDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                AssessmentStatusId = a.AssessmentStatusId,
                AssessmentStatusName = a.AssessmentStatusName,
                AssessmentTypeId = a.AssessmentTypeId,
                AssessmentTypeName = a.AssessmentTypeName,
                QuestionCount = a.QuestionCount,
                CreatedAt = a.CreatedAt,
            }).ToList();
        }

        public async Task<AdminAssessmentListItemDto?> GetAssessmentByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _adminData.GetAssessmentByIdAsync(id, cancellationToken);
            if (a == null)
                return null;
            return new AdminAssessmentListItemDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                AssessmentStatusId = a.AssessmentStatusId,
                AssessmentStatusName = a.AssessmentStatusName,
                AssessmentTypeId = a.AssessmentTypeId,
                AssessmentTypeName = a.AssessmentTypeName,
                QuestionCount = a.QuestionCount,
                CreatedAt = a.CreatedAt,
            };
        }

        public Task<bool> DeleteAssessmentAsync(int id, CancellationToken cancellationToken = default)
            => _adminData.TryDeleteAssessmentAsync(id, cancellationToken);

        public async Task<AdminAnalyticsDto> GetAnalyticsAsync(int trendDays, CancellationToken cancellationToken = default)
        {
            var a = await _adminData.GetAnalyticsAsync(trendDays, cancellationToken);
            return new AdminAnalyticsDto
            {
                AverageCompletionRatePercent = a.AverageCompletionRatePercent,
                DailyAssessmentsTrend = a.DailyAssessmentsTrend
                    .Select(x => new ChartPointDateDto { Date = x.Date.ToString("yyyy-MM-dd"), Count = x.Count })
                    .ToList(),
                MostActiveUsers = a.MostActiveUsers.Select(x => new ActiveUserRankDto
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    Email = x.Email,
                    AssessmentCount = x.AssessmentCount,
                }).ToList(),
                MostUsedQuestions = a.MostUsedQuestions.Select(x => new QuestionUsageRankDto
                {
                    QuestionId = x.QuestionId,
                    QuestionText = x.QuestionText,
                    AnswerCount = x.AnswerCount,
                }).ToList(),
                MostSelectedOptions = a.MostSelectedOptions.Select(x => new OptionUsageRankDto
                {
                    OptionId = x.OptionId,
                    OptionText = x.OptionText,
                    SelectionCount = x.SelectionCount,
                }).ToList(),
            };
        }

        public async Task<AdminSystemHealthDto> GetSystemHealthAsync(CancellationToken cancellationToken = default)
        {
            var db = await _adminData.GetDatabaseHealthAsync(cancellationToken);
            var proc = Process.GetCurrentProcess();
            var uptime = DateTime.UtcNow - proc.StartTime.ToUniversalTime();

            var apiHealthy = db.CanConnect;
            return new AdminSystemHealthDto
            {
                ApiStatus = apiHealthy ? "healthy" : "unhealthy",
                DatabaseConnected = db.CanConnect,
                DatabaseStatus = db.CanConnect ? "connected" : "unavailable",
                DatabaseError = db.ErrorMessage,
                WorkingSetBytes = proc.WorkingSet64,
                GcTotalMemoryBytes = GC.GetTotalMemory(false),
                UptimeSeconds = Math.Round(uptime.TotalSeconds, 2),
                TotalHttpRequests = _requestMetrics.TotalHttpRequests,
            };
        }
    }
}
