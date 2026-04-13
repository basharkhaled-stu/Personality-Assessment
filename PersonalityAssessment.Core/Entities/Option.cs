namespace PersonalityAssessment.Core.Entities
{
    public class Option
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }

        public int QuestionId { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Question Question { get; set; }

        public ICollection<OptionPersonalityScore> OptionPersonalityScores { get; set; } = new HashSet<OptionPersonalityScore>();

        public ICollection<UserAnswer> UserAnswers { get; set; } = new HashSet<UserAnswer>();
    }
}

