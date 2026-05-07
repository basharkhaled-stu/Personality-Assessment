namespace PersonalityAssessment.Core.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public int AssessmentStatusId { get; set; }
        public int AssessmentTypeId { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public AssessmentStatus AssessmentStatus { get; set; } = null!;
        public AssessmentType AssessmentType { get; set; } = null!;


        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
