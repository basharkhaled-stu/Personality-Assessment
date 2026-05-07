namespace PersonalityAssessment.Core.Entities
{
    public class Question
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }

        public int AssessmentId { get; set; }



        public int QuestionTypeId { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Assessment Assessment { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; } = new HashSet<UserAnswer>();


        public ICollection<Option> Options { get; set; } = new HashSet<Option>();


    }
}
