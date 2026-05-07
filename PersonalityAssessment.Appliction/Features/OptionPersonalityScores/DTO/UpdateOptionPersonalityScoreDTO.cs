using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO
{
    public class UpdateOptionPersonalityScoreDTO
    {

        public int Id { get; set; }
        public int OptionId { get; set; }
        public int PersonalityTypeId { get; set; }

        public decimal? Score { get; set; }

      





    }
}
