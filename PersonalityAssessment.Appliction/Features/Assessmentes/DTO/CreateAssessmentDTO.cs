namespace PersonalityAssessment.Application.Features.Assessmentes.DTO
{
    public class CreateAssessmentDTO
    {

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public int AssessmentStatusId { get; set; }
        public int AssessmentTypeId { get; set; }







    }
}
