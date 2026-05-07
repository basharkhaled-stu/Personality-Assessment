using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Assessmentes.Commands.Handlers
{
    public class CreateFullAssessmentCommandHandler
        : IRequestHandler<CreateFullAssessmentCommand, ReadAssessmentDTO>
    {
        private readonly IRepository<Assessment> _assessmentRepo;
        private readonly IRepository<Question> _questionRepo;
        private readonly IRepository<Option> _optionRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFullAssessmentCommandHandler(
            IRepository<Assessment> assessmentRepo,
            IRepository<Question> questionRepo,
            IRepository<Option> optionRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _assessmentRepo = assessmentRepo;
            _questionRepo = questionRepo;
            _optionRepo = optionRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadAssessmentDTO> Handle(
            CreateFullAssessmentCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.DTO;

            // 1. Create Assessment
            var assessment = new Assessment
            {
                Title = dto.Title,
                Description = dto.Description,
                AssessmentStatusId = dto.AssessmentStatusId,
                AssessmentTypeId = dto.AssessmentTypeId,
                CreatedAt = DateTime.UtcNow,
            };

            await _assessmentRepo.AddAsync(assessment);
            await _unitOfWork.SaveChangesAsync(); // get assessment.Id

            // 2. Create Questions and Options
            foreach (var qDto in dto.Questions)
            {
                var question = new Question
                {
                    Text = qDto.Text,
                    DisplayOrder = qDto.DisplayOrder,
                    QuestionTypeId = qDto.QuestionTypeId,
                    AssessmentId = assessment.Id,
                    CreatedAt = DateTime.UtcNow,
                };

                await _questionRepo.AddAsync(question);
                await _unitOfWork.SaveChangesAsync(); // get question.Id

                foreach (var oDto in qDto.Options)
                {
                    var option = new Option
                    {
                        Text = oDto.Text,
                        DisplayOrder = oDto.DisplayOrder,
                        QuestionId = question.Id,
                        CreatedAt = DateTime.UtcNow,
                    };

                    await _optionRepo.AddAsync(option);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            return new ReadAssessmentDTO
            {
                id = assessment.Id,
                Title = assessment.Title,
                Description = assessment.Description,
            };
        }
    }
}
