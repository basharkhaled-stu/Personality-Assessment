namespace PersonalityAssessment.Core.Entities
{
    public class OptionPersonalityScore
    {

        public int Id { get; set; }
        public int OptionId { get; set; }
        public int PersonalityTypeId { get; set; }

        public int? Score { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public PersonalityType PersonalityType { get; set; }
        public Option Option { get; set; }



    }
}
