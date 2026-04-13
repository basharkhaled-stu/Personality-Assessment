namespace PersonalityAssessment.Application.Features.Assessmentes.DTO
{
    public class AdmainReadAssessmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AssessmentStatusName { get; set; }
        public string AssessmentTypeName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }
    }
}
