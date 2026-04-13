using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Services
{
    public interface IPersonalityCalculationService
    {
        List<PersonalityScoreResult> CalculateScores(List<OptionPersonalityScore> personalityScores);
        List<PersonalityScoreResult> RankPersonalities(List<PersonalityScoreResult> scores);
        string GenerateResultCode(List<PersonalityScoreResult> topPersonalities);
        decimal CalculatePercentage(decimal score, decimal totalScore);
    }

    public class PersonalityScoreResult
    {
        public int PersonalityTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
        public int Rank { get; set; }
    }
}
