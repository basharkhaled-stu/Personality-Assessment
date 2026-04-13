namespace PersonalityAssessment.Core.Entities
{
    public class PersonalityType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }



        public ICollection<OptionPersonalityScore> OptionPersonalityScores { get; set; } = new HashSet<OptionPersonalityScore>();
        public ICollection<Strength> strengths { get; set; } = new HashSet<Strength>();

        public ICollection<Weakness> Weaknesses { get; set; } = new HashSet<Weakness>();
        public ICollection<UsersAssessmentResultPersonalityType> UsersAssessmentResultPersonalityType { get; set; }
        = new List<UsersAssessmentResultPersonalityType>();
    }
}
