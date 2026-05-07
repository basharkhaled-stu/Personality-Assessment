namespace PersonalityAssessment.Core.Admin
{
    public sealed class AdminDashboardOverviewData
    {
        public int TotalUsers { get; init; }
        public int TotalAssessments { get; init; }
        public int TotalCompletedAssessments { get; init; }
        public int TotalPendingAssessments { get; init; }
        public int TotalQuestions { get; init; }
        public int TotalPersonalityTypes { get; init; }
        public int TotalAnswersSubmitted { get; init; }
        public string? MostCommonPersonalityTypeName { get; init; }
        public int? MostCommonPersonalityTypeCount { get; init; }
        public decimal? AverageAssessmentScore { get; init; }
    }

    public sealed class AdminUserSummaryData
    {
        public string Id { get; init; } = string.Empty;
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool IsGoogleAccount { get; init; }
    }

    public sealed class AdminPagedUsersData
    {
        public List<AdminUserSummaryData> Items { get; init; } = new();
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public sealed class AdminUserDetailData
    {
        public string Id { get; init; } = string.Empty;
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool IsGoogleAccount { get; init; }
        public bool PhoneNumberConfirmed { get; init; }
        public bool TwoFactorEnabled { get; init; }
        public bool LockoutEnabled { get; init; }
        public DateTimeOffset? LockoutEnd { get; init; }
        public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
        public DateTime? CreatedAtApprox { get; init; }
    }

    public sealed class AdminAssessmentListItemData
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public int AssessmentStatusId { get; init; }
        public string? AssessmentStatusName { get; init; }
        public int AssessmentTypeId { get; init; }
        public string? AssessmentTypeName { get; init; }
        public int QuestionCount { get; init; }
        public DateTime CreatedAt { get; init; }
    }

    public sealed class AdminAssessmentDetailData
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public int AssessmentStatusId { get; init; }
        public string? AssessmentStatusName { get; init; }
        public int AssessmentTypeId { get; init; }
        public string? AssessmentTypeName { get; init; }
        public int QuestionCount { get; init; }
        public DateTime CreatedAt { get; init; }
    }

    public sealed class DailyAssessmentTrendPoint
    {
        public DateOnly Date { get; init; }
        public int Count { get; init; }
    }

    public sealed class ActiveUserRankData
    {
        public string UserId { get; init; } = string.Empty;
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public int AssessmentCount { get; init; }
    }

    public sealed class QuestionUsageRankData
    {
        public int QuestionId { get; init; }
        public string QuestionText { get; init; } = string.Empty;
        public int AnswerCount { get; init; }
    }

    public sealed class OptionUsageRankData
    {
        public int OptionId { get; init; }
        public string OptionText { get; init; } = string.Empty;
        public int SelectionCount { get; init; }
    }

    public sealed class AdminAnalyticsData
    {
        public List<DailyAssessmentTrendPoint> DailyAssessmentsTrend { get; init; } = new();
        public List<ActiveUserRankData> MostActiveUsers { get; init; } = new();
        public List<QuestionUsageRankData> MostUsedQuestions { get; init; } = new();
        public List<OptionUsageRankData> MostSelectedOptions { get; init; } = new();
        public double AverageCompletionRatePercent { get; init; }
    }

    public sealed class AdminDatabaseHealthData
    {
        public bool CanConnect { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
