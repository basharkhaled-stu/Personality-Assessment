using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO
{
    public class AdmainReadOptionPersonalityScoreDTO
    {

        public int Id { get; set; }
        public string OptionName { get; set; }
        public string PersonalityTypeName { get; set; }

        public decimal? Score { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }

        



    }
}
