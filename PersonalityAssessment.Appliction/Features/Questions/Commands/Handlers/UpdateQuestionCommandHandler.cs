using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Questions.Commands.Handlers
{
    public class UpdateQuestionCommandHandler :
        IRequestHandler<UpdateQuestionCommand, bool>
    {
        // BUG FIX: was IRepository<UsersAssessmentResultPersonalityType> (completely wrong entity!)
        private readonly IRepository<Question> _repositoryQuestion;
        private readonly IRepository<QuestionType> _repositoryQuestionType;
        private readonly IRepository<Assessment> _repositoryAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateQuestionCommandHandler(
            IRepository<Question> repositoryQuestion,
            IRepository<QuestionType> repositoryQuestionType,
            IRepository<Assessment> repositoryAssessment,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repositoryQuestion = repositoryQuestion;
            _repositoryQuestionType = repositoryQuestionType;
            _repositoryAssessment = repositoryAssessment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var assessment = await _repositoryAssessment.GetByIdAsync(request.dto.AssessmentId);
            if (assessment == null)
                throw new NotFoundException("Assessment not found");

            var questionType = await _repositoryQuestionType.GetByIdAsync(request.dto.QuestionTypeId);
            if (questionType == null)
                throw new NotFoundException("QuestionType not found");

            var question = await _repositoryQuestion.GetByIdAsync(request.dto.Id);
            if (question == null)
                throw new NotFoundException("Question not found");

            _mapper.Map(request.dto, question);
            question.UpdatedAt = DateTime.UtcNow;
            _repositoryQuestion.Update(question);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
