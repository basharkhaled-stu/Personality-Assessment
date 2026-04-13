namespace PersonalityAssessment.Application.Features.UsersAssessments.DTO
{
    public class SubmitAssessmentDTO
    {
        public int UsersAssessmentId { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }

    public class AnswerDTO
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
    }
}
