namespace PersonalityAssessment.Application.Features.Questions.DTO
{
    public class AdmainReadQuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }

        public string AssessmentName { get; set; }



        public string QuestionTypeName { get; set; }



        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }
    }
}
