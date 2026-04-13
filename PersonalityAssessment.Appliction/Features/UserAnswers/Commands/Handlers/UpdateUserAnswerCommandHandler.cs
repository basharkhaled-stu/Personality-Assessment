using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands.Handlers
{
    public class UpdateUserAnswerCommandHandler :
        IRequestHandler<UpdateUserAnswerCommand, bool>
    {
        private readonly IRepository<Option> _repositoryOption;
        private readonly IRepository< Question> _repositoryQuestion;
        private readonly IRepository<UserAnswer> _repositoryUserAnswer;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserAnswerCommandHandler(
           IRepository<Option> repositoryOption,
           IRepository< Question> repositoryQuestion,
           IRepository<UserAnswer> repositoryUserAnswer,
           IRepository<UsersAssessment> repositoryUsersAssessment,

        IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repositoryOption = repositoryOption;
            _repositoryQuestion = repositoryQuestion;
            _repositoryUserAnswer = repositoryUserAnswer;
            _repositoryUsersAssessment = repositoryUsersAssessment;

            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }



        public async Task<bool> Handle
            (UpdateUserAnswerCommand request,
            CancellationToken cancellationToken)
        {

            var resultOption = await _repositoryOption.GetByIdAsync(request.dto.OptionId);

            if (resultOption == null)
            {
                throw new NotFoundException("Option not found");
            }
            var resultQuestion = await _repositoryQuestion.GetByIdAsync(request.dto.QuestionId);
            if (resultQuestion == null)
            {
                throw new NotFoundException("Question not found");
            }

            var resultUsersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.dto.UsersAssessmentId);
            if (resultQuestion == null)
            {
                throw new NotFoundException("UsersAssessment not found");
            }

            var result = await _repositoryUserAnswer.GetByIdAsync(request.id);
            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repositoryUserAnswer.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
