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
        private readonly IRepository<QuestionType> _repositoryQuestionType;
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repositoryQuestion;
        private readonly IRepository<Assessment> _repositoryAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;



        public UpdateQuestionCommandHandler(
           IRepository<QuestionType> repositoryQuestionType,
           IRepository<UsersAssessmentResultPersonalityType> repositoryQuestion,
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





        public async Task<bool> Handle
            (UpdateQuestionCommand request,
            CancellationToken cancellationToken)
        {

            var ResultAssessment = await _repositoryAssessment.GetByIdAsync(request.dto.AssessmentId);
            var ResultQuestionType = await _repositoryQuestionType.GetByIdAsync(request.dto.QuestionTypeId);

            if (ResultAssessment == null)
            {
                throw new NotFoundException("Assessment not found");
            }

            if (ResultQuestionType == null)
            {
                throw new NotFoundException("QuestionType not found");
            }

            var result = await _repositoryQuestion.GetByIdAsync(request.dto.Id);

            if (result == null)
            {
                throw new NotFoundException("Question not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repositoryQuestion.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
