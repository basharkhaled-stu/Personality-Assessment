namespace PersonalityAssessment.Application.Features.QuestionTypes.DTO
{
    public class AdmainReadQuestionTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }


    }
}
