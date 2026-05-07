namespace PersonalityAssessment.Core.Entities
{
    public class UsersAssessmentResult
    {




        public int Id { get; set; }
        public int UsersAssessmentId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public UsersAssessment usersAssessment { get; set; }

        public ICollection<UsersAssessmentResultPersonalityType> UsersAssessmentResultPersonalityType { get; set; }
     = new List<UsersAssessmentResultPersonalityType>();
    }
}
