namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminAssessmentListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int AssessmentStatusId { get; set; }
        public string? AssessmentStatusName { get; set; }
        public int AssessmentTypeId { get; set; }
        public string? AssessmentTypeName { get; set; }
        public int QuestionCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
