namespace PersonalityAssessment.Application.Features.UserAnswers.DTO
{
    public class AdmainReadUserAnswerDTO
    {
        public int Id { get; set; }
        public string UsersAssessmentName { get; set; }
        public string QuestionName { get; set; }
        public string OptionName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }


    }
}
