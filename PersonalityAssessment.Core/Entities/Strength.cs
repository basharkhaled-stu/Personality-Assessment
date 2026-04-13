namespace PersonalityAssessment.Core.Entities
{
    public class Strength
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int PersonalityTypeId { get; set; }

        public PersonalityType PersonalityType { get; set; }
    }
}
