namespace PersonalityAssessment.Core.Entities
{
    public class UserAnswer
    {

        public int Id { get; set; }
        public int UsersAssessmentId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public UsersAssessment UsersAssessment { get; set; }
        public Question Question { get; set; }
        public Option Option { get; set; }


    }
}
