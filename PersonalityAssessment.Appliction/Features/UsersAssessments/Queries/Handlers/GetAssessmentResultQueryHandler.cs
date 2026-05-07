using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Application.Services;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries.Handlers
{
    public class GetAssessmentResultQueryHandler
        : IRequestHandler<GetAssessmentResultQuery, AssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessment> _usersAssessmentRepository;
        private readonly IUsersAssessmentRepository _customUsersAssessmentRepository;
        private readonly IPersonalityCalculationService _personalityCalculationService;

        public GetAssessmentResultQueryHandler(
            IRepository<UsersAssessment> usersAssessmentRepository,
            IUsersAssessmentRepository customUsersAssessmentRepository,
            IPersonalityCalculationService personalityCalculationService)
        {
            _usersAssessmentRepository = usersAssessmentRepository;
            _customUsersAssessmentRepository = customUsersAssessmentRepository;
            _personalityCalculationService = personalityCalculationService;
        }

        public async Task<AssessmentResultDTO> Handle(
            GetAssessmentResultQuery request,
            CancellationToken cancellationToken)
        {
            var assessment = await _customUsersAssessmentRepository.GetByIdWithResultsAsync(request.UsersAssessmentId);
            if (assessment == null)
                throw new NotFoundException("Assessment not found");

            var assessmentResult = assessment.UsersAssessmentResults.FirstOrDefault();
            if (assessmentResult == null)
                throw new NotFoundException("Assessment result not found - submit the assessment first");

            var personalityResults = assessmentResult.UsersAssessmentResultPersonalityType
                .OrderBy(x => x.Rank)
                .ToList();

            if (!personalityResults.Any())
                throw new NotFoundException("No personality results found");

            var allPersonalityTypes = await _customUsersAssessmentRepository.GetAllPersonalityTypesAsync();
            var allTypesDict = allPersonalityTypes.ToDictionary(pt => pt.Id);

            // BUG FIX: use TryGetValue instead of .First() to avoid NullReferenceException
            var dashboardResults = allPersonalityTypes.Select(pt =>
            {
                var result = personalityResults.FirstOrDefault(pr => pr.PersonalityTypeId == pt.Id);
                return new PersonalityScoreResult
                {
                    PersonalityTypeId = pt.Id,
                    Name  = pt.Name  ?? "",
                    Label = pt.Label ?? "",
                    Score = result?.Score ?? 0m,
                    Rank  = result?.Rank  ?? 0
                };
            }).ToList();

            var totalScore = dashboardResults.Sum(x => x.Score);
            foreach (var r in dashboardResults)
                r.Percentage = _personalityCalculationService.CalculatePercentage(r.Score, totalScore);

            var topPersonalities = personalityResults.Take(2).ToList();
            var topPersonalityTypeIds = topPersonalities.Select(x => x.PersonalityTypeId).ToList();

            var strengths  = await _customUsersAssessmentRepository.GetStrengthsByPersonalityTypeIdsAsync(topPersonalityTypeIds);
            var weaknesses = await _customUsersAssessmentRepository.GetWeaknessesByPersonalityTypeIdsAsync(topPersonalityTypeIds);

            // BUG FIX: use dict lookup with null fallback instead of .First()
            var topScoreResults = topPersonalities.Select(tp =>
            {
                allTypesDict.TryGetValue(tp.PersonalityTypeId, out var pt);
                return new PersonalityScoreResult
                {
                    PersonalityTypeId = tp.PersonalityTypeId,
                    Name  = pt?.Name  ?? "",
                    Label = pt?.Label ?? "",
                    Score = tp.Score,
                    Rank  = tp.Rank
                };
            }).ToList();

            var resultCode = _personalityCalculationService.GenerateResultCode(topScoreResults);

            var response = new AssessmentResultDTO
            {
                ResultCode = resultCode,
                TopPersonalities = topScoreResults.Select(p => new TopPersonalityDTO
                {
                    PersonalityTypeId = p.PersonalityTypeId,
                    Name       = p.Name,
                    Label      = p.Label,
                    Score      = p.Score,
                    Percentage = _personalityCalculationService.CalculatePercentage(p.Score, totalScore),
                    Strengths  = strengths .Where(s => s.PersonalityTypeId == p.PersonalityTypeId).Select(s => s.Text).ToList(),
                    Weaknesses = weaknesses.Where(w => w.PersonalityTypeId == p.PersonalityTypeId).Select(w => w.Text).ToList()
                }).ToList(),
                Dashboard = dashboardResults
                    .OrderByDescending(x => x.Score)
                    .Select(p => new DashboardPersonalityDTO
                    {
                        PersonalityTypeId = p.PersonalityTypeId,
                        Name       = p.Name,
                        Label      = p.Label,
                        Score      = p.Score,
                        Percentage = p.Percentage
                    }).ToList()
            };

            return response;
        }
    }
}
