namespace PersonalityAssessment.Application.Features.Assessmentes.DTO
{
    public class UpdateAssessmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public int AssessmentStatusId { get; set; }
        public int AssessmentTypeId { get; set; }




    }
}
