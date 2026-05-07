using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO
{


    public class ReadOptionPersonalityScoreDTO
    {
        public int Id { get; set; }
        public string  OptionName { get; set; }
        public string PersonalityTypeName { get; set; }

        public decimal? Score { get; set; }

       


    }
}
