using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Services
{
    public class PersonalityCalculationService : IPersonalityCalculationService
    {
        public List<PersonalityScoreResult> CalculateScores(List<OptionPersonalityScore> personalityScores)
        {
            var groupedScores = personalityScores
                .Where(x => x.Score.HasValue)
                .GroupBy(x => x.PersonalityTypeId)
                .Select(g => new PersonalityScoreResult
                {
                    PersonalityTypeId = g.Key,
                    Score = g.Sum(x => x.Score!.Value),
                    Name = g.First().PersonalityType.Name,
                    Label = g.First().PersonalityType.Label
                })
                .ToList();

            return groupedScores;
        }

        public List<PersonalityScoreResult> RankPersonalities(List<PersonalityScoreResult> scores)
        {
            var rankedScores = scores
                .OrderByDescending(x => x.Score)
                .ToList();

            var totalScore = rankedScores.Sum(x => x.Score);

            for (int i = 0; i < rankedScores.Count; i++)
            {
                rankedScores[i].Rank = i + 1;
                rankedScores[i].Percentage = CalculatePercentage(rankedScores[i].Score, totalScore);
            }

            return rankedScores;
        }

        public string GenerateResultCode(List<PersonalityScoreResult> topPersonalities)
        {
            if (topPersonalities == null || topPersonalities.Count == 0)
                return string.Empty;

            var topTwo = topPersonalities
                .OrderByDescending(x => x.Score)
                .Take(2)
                .OrderBy(x => x.Rank)
                .ToList();

            return string.Join("", topTwo.Select(x => x.Label));
        }

        public decimal CalculatePercentage(decimal score, decimal totalScore)
        {
            if (totalScore == 0) return 0;
            return Math.Round((score / totalScore) * 100, 2);
        }
    }
}
