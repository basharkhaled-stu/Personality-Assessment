namespace PersonalityAssessment.Application.Features.Strengths.DTO
{
    public class AdmainReadStrengthDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string PersonalityTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }





    }
}
