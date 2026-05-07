namespace PersonalityAssessment.Application.Features.PersonalityTypes.DTO
{
    public class AdmainPersonalityTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }





    }
}
