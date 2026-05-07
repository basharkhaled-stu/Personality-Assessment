using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands.Handlers
{
    public class CreateUserAnswerCommandHandler
        : IRequestHandler<CreateUserAnswerCommand, ReadUserAnswerDTO>
    {
        private readonly IRepository<Option> _repositoryOption;
        private readonly IRepository<Question> _repositoryQuestion;
        private readonly IRepository<UserAnswer> _repositoryUserAnswer;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserAnswerCommandHandler(
            IRepository<Option> repositoryOption,
            IRepository<Question> repositoryQuestion,
            IRepository<UserAnswer> repositoryUserAnswer,
            IRepository<UsersAssessment> repositoryUsersAssessment,
            IIdentityService identityService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repositoryOption = repositoryOption;
            _repositoryQuestion = repositoryQuestion;
            _repositoryUserAnswer = repositoryUserAnswer;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadUserAnswerDTO> Handle(CreateUserAnswerCommand request, CancellationToken cancellationToken)
        {
            var option = await _repositoryOption.GetByIdAsync(request.DTO.OptionId);
            if (option == null) throw new NotFoundException("Option not found");

            var question = await _repositoryQuestion.GetByIdAsync(request.DTO.QuestionId);
            if (question == null) throw new NotFoundException("Question not found");

            var usersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.DTO.UsersAssessmentId);
            // BUG FIX: was checking resultQuestion instead of resultUsersAssessment
            if (usersAssessment == null) throw new NotFoundException("UsersAssessment not found");

            var entity = _mapper.Map<UserAnswer>(request.DTO);
            entity.Option = option;
            entity.Question = question;
            entity.UsersAssessment = usersAssessment;

            await _repositoryUserAnswer.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<ReadUserAnswerDTO>(entity);
            // BUG FIX: pass userId not username
            try { dto.UsersAssessmentName = await _identityService.GetFullNameAsync(usersAssessment.UserId) ?? ""; }
            catch { dto.UsersAssessmentName = ""; }
            return dto;
        }
    }
}
