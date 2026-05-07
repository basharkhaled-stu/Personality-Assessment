namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class ChartPointDateDto
    {
        public string Date { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class ActiveUserRankDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int AssessmentCount { get; set; }
    }

    public class QuestionUsageRankDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int AnswerCount { get; set; }
    }

    public class OptionUsageRankDto
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int SelectionCount { get; set; }
    }

    public class AdminAnalyticsDto
    {
        public List<ChartPointDateDto> DailyAssessmentsTrend { get; set; } = new();
        public List<ActiveUserRankDto> MostActiveUsers { get; set; } = new();
        public List<QuestionUsageRankDto> MostUsedQuestions { get; set; } = new();
        public List<OptionUsageRankDto> MostSelectedOptions { get; set; } = new();
        public double AverageCompletionRatePercent { get; set; }
    }
}
