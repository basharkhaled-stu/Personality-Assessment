namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminDashboardOverviewDto
    {
        public int TotalUsers { get; set; }
        public int TotalAssessments { get; set; }
        public int TotalCompletedAssessments { get; set; }
        public int TotalPendingAssessments { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalPersonalityTypes { get; set; }
        public int TotalAnswersSubmitted { get; set; }
        public string? MostCommonPersonalityTypeName { get; set; }
        public int? MostCommonPersonalityTypeCount { get; set; }
        public decimal? AverageAssessmentScore { get; set; }
    }
}
