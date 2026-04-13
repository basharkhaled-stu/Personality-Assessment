namespace PersonalityAssessment.Application.Features.UsersAssessments.DTO
{
    public class AssessmentResultDTO
    {
        public string ResultCode { get; set; } = string.Empty;
        public List<TopPersonalityDTO> TopPersonalities { get; set; } = new List<TopPersonalityDTO>();
        public List<DashboardPersonalityDTO> Dashboard { get; set; } = new List<DashboardPersonalityDTO>();
    }

    public class TopPersonalityDTO
    {
        public int PersonalityTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
        public List<string> Strengths { get; set; } = new List<string>();
        public List<string> Weaknesses { get; set; } = new List<string>();
    }

    public class DashboardPersonalityDTO
    {
        public int PersonalityTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
    }
}
