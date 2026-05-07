using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.Questions.Commands.Handlers
{
    public class CreateQuestionCommandHandler
        : IRequestHandler<CreateQuestionCommand, ReadQuestionDTO>
    {
        private readonly IRepository<QuestionType> _repositoryQuestionType;
        private readonly IRepository<Question> _repositoryQuestion;
        private readonly IRepository<Assessment> _repositoryAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(
            IRepository<QuestionType> repositoryQuestionType,
            IRepository<Question> repositoryQuestion,
            IRepository<Assessment> repositoryAssessment,

            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repositoryQuestion = repositoryQuestion;
            _repositoryQuestionType = repositoryQuestionType;
            _repositoryAssessment = repositoryAssessment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }



        public async Task<ReadQuestionDTO> Handle
            (CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {

            var ResultAssessment = await _repositoryAssessment.GetByIdAsync(request.DTO.AssessmentId);
            var ResultQuestionType = await _repositoryQuestionType.GetByIdAsync(request.DTO.QuestionTypeId);

            if (ResultAssessment == null)
            {
                throw new NotFoundException("Assessment not found");
            }

            if (ResultQuestionType == null)
            {
                throw new NotFoundException("QuestionType not found");
            }
            int max = _repositoryQuestion.GetAll()
             .Where(a => a.AssessmentId == request.DTO.AssessmentId)
             .Select(a => (int?)a.DisplayOrder)
               .Max() ?? 0;

            var result = _mapper.Map<Question>(request.DTO);
            result.DisplayOrder = max + 1;
            result.Assessment = ResultAssessment;
            result.QuestionType = ResultQuestionType;



            await _repositoryQuestion.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadQuestionDTO>(result);
        }
    }
}
