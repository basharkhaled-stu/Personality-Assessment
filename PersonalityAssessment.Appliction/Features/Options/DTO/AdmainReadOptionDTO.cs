namespace PersonalityAssessment.Application.Features.Options.DTO
{
    public class AdmainReadOptionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }

        public string QuestionName { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }







    }
}
