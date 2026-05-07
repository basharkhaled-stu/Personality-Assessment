namespace PersonalityAssessment.Core.Entities
{
    public class UsersAssessmentResultPersonalityType
    {
        public int Id { get; set; }
        public int PersonalityTypeId { get; set; }
        public int UsersAssessmentResultId { get; set; }

        public decimal Score { get; set; }
        public int Rank { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UsersAssessmentResult UsersAssessmentResult { get; set; }
        public PersonalityType PersonalityType { get; set; }
    }
}
