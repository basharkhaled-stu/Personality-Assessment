namespace PersonalityAssessment.Application.Features.Weaknesses.DTO
{
    public class AdmainReadWeaknessDTO
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
