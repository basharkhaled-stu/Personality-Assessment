using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Services
{
    public class PersonalityCalculationService : IPersonalityCalculationService
    {
        public List<PersonalityScoreResult> CalculateScores(List<OptionPersonalityScore> personalityScores)
        {
            // BUG FIX: null-safe access on PersonalityType navigation property
            return personalityScores
                .Where(x => x.Score.HasValue)
                .GroupBy(x => x.PersonalityTypeId)
                .Select(g => new PersonalityScoreResult
                {
                    PersonalityTypeId = g.Key,
                    Score = g.Sum(x => x.Score!.Value),
                    Name  = g.First().PersonalityType?.Name  ?? "",
                    Label = g.First().PersonalityType?.Label ?? "",
                })
                .ToList();
        }

        public List<PersonalityScoreResult> RankPersonalities(List<PersonalityScoreResult> scores)
        {
            var ranked = scores.OrderByDescending(x => x.Score).ToList();
            var total  = ranked.Sum(x => x.Score);
            for (int i = 0; i < ranked.Count; i++)
            {
                ranked[i].Rank       = i + 1;
                ranked[i].Percentage = CalculatePercentage(ranked[i].Score, total);
            }
            return ranked;
        }

        public string GenerateResultCode(List<PersonalityScoreResult> topPersonalities)
        {
            if (topPersonalities == null || topPersonalities.Count == 0)
                return string.Empty;

            return string.Join("", topPersonalities
                .OrderByDescending(x => x.Score)
                .Take(2)
                .OrderBy(x => x.Rank)
                .Select(x => x.Label ?? ""));
        }

        public decimal CalculatePercentage(decimal score, decimal totalScore)
        {
            if (totalScore == 0) return 0;
            return Math.Round((score / totalScore) * 100, 2);
        }
    }
}
