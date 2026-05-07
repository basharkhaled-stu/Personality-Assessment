namespace PersonalityAssessment.Application.Features.Assessmentes.DTO
{
    public class CreateFullAssessmentDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int AssessmentStatusId { get; set; }
        public int AssessmentTypeId { get; set; }
        public List<CreateFullQuestionDTO> Questions { get; set; } = new();
    }

    public class CreateFullQuestionDTO
    {
        public string Text { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public int QuestionTypeId { get; set; }
        public List<CreateFullOptionDTO> Options { get; set; } = new();
    }

    public class CreateFullOptionDTO
    {
        public string Text { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
