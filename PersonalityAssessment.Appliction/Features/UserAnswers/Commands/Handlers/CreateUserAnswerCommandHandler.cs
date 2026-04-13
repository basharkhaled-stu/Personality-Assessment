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
        private readonly IRepository< Question> _repositoryQuestion;
        private readonly IRepository<UserAnswer> _repositoryUserAnswer;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserAnswerCommandHandler(
           IRepository<Option> repositoryOption,
           IRepository< Question> repositoryQuestion,
           IRepository<UserAnswer> repositoryUserAnswer,
           IRepository<UsersAssessment> repositoryUsersAssessment,
         IIdentityService identityService,
        IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repositoryOption = repositoryOption;
            _repositoryQuestion = repositoryQuestion;
            _repositoryUserAnswer = repositoryUserAnswer;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }



        public async Task<ReadUserAnswerDTO> Handle
            (CreateUserAnswerCommand request,
            CancellationToken cancellationToken)
        {

            var resultOption = await _repositoryOption.GetByIdAsync(request.DTO.OptionId);

            if (resultOption == null)
            {
                throw new NotFoundException("Option not found");
            }
            var resultQuestion = await _repositoryQuestion.GetByIdAsync(request.DTO.QuestionId);
            if (resultQuestion == null)
            {
                throw new NotFoundException("Question not found");
            }

            var resultUsersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.DTO.UsersAssessmentId);
            if (resultQuestion == null)
            {
                throw new NotFoundException("UsersAssessment not found");
            }



            var result = _mapper.Map<UserAnswer>(request.DTO);

            result.Option = resultOption;
            result.Question = resultQuestion;
            result.UsersAssessment = resultUsersAssessment;


            await _repositoryUserAnswer.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<ReadUserAnswerDTO>(result);
            dto.UsersAssessmentName = await _identityService.GetFullNameAsync(dto.UsersAssessmentName);
            return dto;
        }
    }
}
