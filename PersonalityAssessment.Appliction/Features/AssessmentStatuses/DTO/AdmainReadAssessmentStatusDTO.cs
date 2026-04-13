namespace PersonalityAssessment.Application.Features.AssessmentStatuses.DTO
{
    public class AdmainReadAssessmentStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }
    }
}
