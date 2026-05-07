using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands.Handlers
{
    public class UpdateUserAnswerCommandHandler : IRequestHandler<UpdateUserAnswerCommand, bool>
    {
        private readonly IRepository<Option> _repositoryOption;
        private readonly IRepository<Question> _repositoryQuestion;
        private readonly IRepository<UserAnswer> _repositoryUserAnswer;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserAnswerCommandHandler(
            IRepository<Option> repositoryOption,
            IRepository<Question> repositoryQuestion,
            IRepository<UserAnswer> repositoryUserAnswer,
            IRepository<UsersAssessment> repositoryUsersAssessment,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryOption = repositoryOption;
            _repositoryQuestion = repositoryQuestion;
            _repositoryUserAnswer = repositoryUserAnswer;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserAnswerCommand request, CancellationToken cancellationToken)
        {
            var option = await _repositoryOption.GetByIdAsync(request.dto.OptionId);
            if (option == null) throw new NotFoundException("Option not found");

            var question = await _repositoryQuestion.GetByIdAsync(request.dto.QuestionId);
            if (question == null) throw new NotFoundException("Question not found");

            var usersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.dto.UsersAssessmentId);
            // BUG FIX: was checking resultQuestion instead of resultUsersAssessment
            if (usersAssessment == null) throw new NotFoundException("UsersAssessment not found");

            var entity = await _repositoryUserAnswer.GetByIdAsync(request.id);
            // BUG FIX: missing null check on the UserAnswer itself
            if (entity == null) throw new NotFoundException("UserAnswer not found");

            _mapper.Map(request.dto, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repositoryUserAnswer.Update(entity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
