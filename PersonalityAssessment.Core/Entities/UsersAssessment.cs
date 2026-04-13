namespace PersonalityAssessment.Core.Entities
{
    public class UsersAssessment
    {

        public int Id { get; set; }
        public string UserId { get; set; }


        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int UserAssessmentStatusId { get; set; }


        public ICollection<UsersAssessmentResult> UsersAssessmentResults { get; set; } = new HashSet<UsersAssessmentResult>();
        public UserAssessmentStatus UserAssessmentStatus { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; } = new HashSet<UserAnswer>();





    }
}
