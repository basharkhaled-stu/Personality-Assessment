using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Application.Services;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries.Handlers
{
    public class GetAssessmentResultQueryHandler
        : IRequestHandler<GetAssessmentResultQuery, AssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessment> _usersAssessmentRepository;
        private readonly IRepository<UsersAssessmentResult> _usersAssessmentResultRepository;
        private readonly IRepository<UsersAssessmentResultPersonalityType> _usersAssessmentResultPersonalityTypeRepository;
        private readonly IUsersAssessmentRepository _customUsersAssessmentRepository;
        private readonly IPersonalityCalculationService _personalityCalculationService;

        public GetAssessmentResultQueryHandler(
            IRepository<UsersAssessment> usersAssessmentRepository,
            IRepository<UsersAssessmentResult> usersAssessmentResultRepository,
            IRepository<UsersAssessmentResultPersonalityType> usersAssessmentResultPersonalityTypeRepository,
            IUsersAssessmentRepository customUsersAssessmentRepository,
            IPersonalityCalculationService personalityCalculationService)
        {
            _usersAssessmentRepository = usersAssessmentRepository;
            _usersAssessmentResultRepository = usersAssessmentResultRepository;
            _usersAssessmentResultPersonalityTypeRepository = usersAssessmentResultPersonalityTypeRepository;
            _customUsersAssessmentRepository = customUsersAssessmentRepository;
            _personalityCalculationService = personalityCalculationService;
        }

        public async Task<AssessmentResultDTO> Handle(
            GetAssessmentResultQuery request,
            CancellationToken cancellationToken)
        {
            // Get the assessment with results
            var assessment = await _customUsersAssessmentRepository.GetByIdWithResultsAsync(request.UsersAssessmentId);
            if (assessment == null)
                throw new NotFoundException("Assessment not found");

            // Get the assessment result
            var assessmentResult = assessment.UsersAssessmentResults.FirstOrDefault();
            if (assessmentResult == null)
                throw new NotFoundException("Assessment result not found");

            // Get personality results
            var personalityResults = assessmentResult.UsersAssessmentResultPersonalityType
                .OrderBy(x => x.Rank)
                .ToList();

            if (!personalityResults.Any())
                throw new NotFoundException("Personality results not found");

            // Get all personality types for dashboard
            var allPersonalityTypes = await _customUsersAssessmentRepository.GetAllPersonalityTypesAsync();

            // Build personality score results for dashboard
            var dashboardResults = allPersonalityTypes.Select(pt =>
            {
                var result = personalityResults.FirstOrDefault(pr => pr.PersonalityTypeId == pt.Id);
                return new PersonalityScoreResult
                {
                    PersonalityTypeId = pt.Id,
                    Name = pt.Name,
                    Label = pt.Label,
                    Score = result?.Score ?? 0m,
                    Rank = result?.Rank ?? 0
                };
            }).ToList();

            // Calculate percentages for dashboard
            var totalScore = dashboardResults.Sum(x => x.Score);
            foreach (var result in dashboardResults)
            {
                result.Percentage = _personalityCalculationService.CalculatePercentage(result.Score, totalScore);
            }

            // Get top 2 personalities
            var topPersonalities = personalityResults.Take(2).ToList();
            var topPersonalityTypeIds = topPersonalities.Select(x => x.PersonalityTypeId).ToList();

            // Load strengths and weaknesses
            var strengths = await _customUsersAssessmentRepository.GetStrengthsByPersonalityTypeIdsAsync(topPersonalityTypeIds);
            var weaknesses = await _customUsersAssessmentRepository.GetWeaknessesByPersonalityTypeIdsAsync(topPersonalityTypeIds);

            // Generate result code
            var topPersonalityScoreResults = topPersonalities.Select(tp => new PersonalityScoreResult
            {
                PersonalityTypeId = tp.PersonalityTypeId,
                Name = allPersonalityTypes.First(pt => pt.Id == tp.PersonalityTypeId).Name,
                Label = allPersonalityTypes.First(pt => pt.Id == tp.PersonalityTypeId).Label,
                Score = tp.Score,
                Rank = tp.Rank
            }).ToList();

            var resultCode = _personalityCalculationService.GenerateResultCode(topPersonalityScoreResults);

            // Build response
            var response = new AssessmentResultDTO
            {
                ResultCode = resultCode,
                TopPersonalities = topPersonalityScoreResults.Select(personality => new TopPersonalityDTO
                {
                    PersonalityTypeId = personality.PersonalityTypeId,
                    Name = personality.Name,
                    Label = personality.Label,
                    Score = personality.Score,
                    Percentage = _personalityCalculationService.CalculatePercentage(personality.Score, totalScore),
                    Strengths = strengths
                        .Where(s => s.PersonalityTypeId == personality.PersonalityTypeId)
                        .Select(s => s.Text)
                        .ToList(),
                    Weaknesses = weaknesses
                        .Where(w => w.PersonalityTypeId == personality.PersonalityTypeId)
                        .Select(w => w.Text)
                        .ToList()
                }).ToList(),
                Dashboard = dashboardResults
                    .OrderByDescending(x => x.Score)
                    .Select(personality => new DashboardPersonalityDTO
                    {
                        PersonalityTypeId = personality.PersonalityTypeId,
                        Name = personality.Name,
                        Label = personality.Label,
                        Score = personality.Score,
                        Percentage = personality.Percentage
                    }).ToList()
            };

            return response;
        }
    }
}
