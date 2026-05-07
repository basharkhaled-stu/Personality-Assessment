using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Application.Services;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Handlers
{
    public class SubmitAssessmentCommandHandler
        : IRequestHandler<SubmitAssessmentCommand, AssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessment> _usersAssessmentRepository;
        private readonly IRepository<UserAnswer> _userAnswerRepository;
        private readonly IRepository<UsersAssessmentResult> _usersAssessmentResultRepository;
        private readonly IRepository<UsersAssessmentResultPersonalityType> _usersAssessmentResultPersonalityTypeRepository;
        private readonly IUsersAssessmentRepository _customUsersAssessmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonalityCalculationService _personalityCalculationService;

        public SubmitAssessmentCommandHandler(
            IRepository<UsersAssessment> usersAssessmentRepository,
            IRepository<UserAnswer> userAnswerRepository,
            IRepository<UsersAssessmentResult> usersAssessmentResultRepository,
            IRepository<UsersAssessmentResultPersonalityType> usersAssessmentResultPersonalityTypeRepository,
            IUsersAssessmentRepository customUsersAssessmentRepository,
            IUnitOfWork unitOfWork,
            IPersonalityCalculationService personalityCalculationService)
        {
            _usersAssessmentRepository = usersAssessmentRepository;
            _userAnswerRepository = userAnswerRepository;
            _usersAssessmentResultRepository = usersAssessmentResultRepository;
            _usersAssessmentResultPersonalityTypeRepository = usersAssessmentResultPersonalityTypeRepository;
            _customUsersAssessmentRepository = customUsersAssessmentRepository;
            _unitOfWork = unitOfWork;
            _personalityCalculationService = personalityCalculationService;
        }

        public async Task<AssessmentResultDTO> Handle(
            SubmitAssessmentCommand request,
            CancellationToken cancellationToken)
        {
            // Get the assessment
            var assessment = await _usersAssessmentRepository.GetByIdAsync(request.DTO.UsersAssessmentId);
            if (assessment == null)
                throw new NotFoundException("Assessment not found");

            // Save user answers
            var userAnswers = request.DTO.Answers.Select(answer => new UserAnswer
            {
                UsersAssessmentId = request.DTO.UsersAssessmentId,
                QuestionId = answer.QuestionId,
                OptionId = answer.OptionId,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            foreach (var answer in userAnswers)
            {
                await _userAnswerRepository.AddAsync(answer);
            }

            // Get personality scores for selected options
            var optionIds = request.DTO.Answers.Select(x => x.OptionId).ToList();
            var personalityScores = await _customUsersAssessmentRepository.GetPersonalityScoresByOptionsAsync(optionIds);

            // Calculate and rank personality scores
            var calculatedScores = _personalityCalculationService.CalculateScores(personalityScores);
            var rankedScores = _personalityCalculationService.RankPersonalities(calculatedScores);

            // Generate result code
            var topPersonalities = rankedScores.Take(2).ToList();
            var resultCode = _personalityCalculationService.GenerateResultCode(topPersonalities);

            // Save assessment result
            var assessmentResult = new UsersAssessmentResult
            {
                UsersAssessmentId = request.DTO.UsersAssessmentId,
                CreatedAt = DateTime.UtcNow
            };

            await _usersAssessmentResultRepository.AddAsync(assessmentResult);
            await _unitOfWork.SaveChangesAsync(); // Save to get the ID

            // Save top 2 personality results
            foreach (var personality in topPersonalities)
            {
                var resultPersonalityType = new UsersAssessmentResultPersonalityType
                {
                    UsersAssessmentResultId = assessmentResult.Id,
                    PersonalityTypeId = personality.PersonalityTypeId,
                    Score = personality.Score,
                    Rank = personality.Rank,
                    CreatedAt = DateTime.UtcNow
                };

                await _usersAssessmentResultPersonalityTypeRepository.AddAsync(resultPersonalityType);
            }

            // Update assessment as completed
            var completedStatus = await _customUsersAssessmentRepository.GetCompletedStatusAsync();
            if (completedStatus != null)
            {
                assessment.UserAssessmentStatusId = completedStatus.Id;
            }
            assessment.CompletedAt = DateTime.UtcNow;

            _usersAssessmentRepository.Update(assessment);

            await _unitOfWork.SaveChangesAsync();

            // Load strengths and weaknesses for top personalities
            var personalityTypeIds = topPersonalities.Select(x => x.PersonalityTypeId).ToList();
            var strengths = await _customUsersAssessmentRepository.GetStrengthsByPersonalityTypeIdsAsync(personalityTypeIds);
            var weaknesses = await _customUsersAssessmentRepository.GetWeaknessesByPersonalityTypeIdsAsync(personalityTypeIds);

            // Build response
            var response = new AssessmentResultDTO
            {
                ResultCode = resultCode,
                TopPersonalities = topPersonalities.Select(personality => new TopPersonalityDTO
                {
                    PersonalityTypeId = personality.PersonalityTypeId,
                    Name = personality.Name,
                    Label = personality.Label,
                    Score = personality.Score,
                    Percentage = personality.Percentage,
                    Strengths = strengths
                        .Where(s => s.PersonalityTypeId == personality.PersonalityTypeId)
                        .Select(s => s.Text)
                        .ToList(),
                    Weaknesses = weaknesses
                        .Where(w => w.PersonalityTypeId == personality.PersonalityTypeId)
                        .Select(w => w.Text)
                        .ToList()
                }).ToList(),
                Dashboard = rankedScores.Select(personality => new DashboardPersonalityDTO
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
