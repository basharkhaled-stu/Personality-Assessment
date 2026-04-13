namespace PersonalityAssessment.Application.Features.Assessmentes.DTO
{


    public class ReadAssessmentDTO
    {
        public int id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string AssessmentStatusName { get; set; }
        public string AssessmentTypeName { get; set; }
    }
}
