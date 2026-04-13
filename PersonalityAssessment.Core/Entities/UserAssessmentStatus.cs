namespace PersonalityAssessment.Core.Entities
{
    public class UserAssessmentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<UsersAssessment> UsersAssessments { get; set; }
    }
}
