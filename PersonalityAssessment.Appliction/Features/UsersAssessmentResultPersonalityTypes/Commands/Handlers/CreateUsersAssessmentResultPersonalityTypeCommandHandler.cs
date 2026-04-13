using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands.Handlers
{
    public class CreateUsersAssessmentResultPersonalityTypeCommandHandler
        : IRequestHandler<CreateUsersAssessmentResultPersonalityTypeCommand, ReadUsersAssessmentResultPersonalityTypeDTO>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IRepository<UsersAssessmentResult> _repositoryUsersAssessmentResult;

        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUsersAssessmentResultPersonalityTypeCommandHandler(
           IRepository<UsersAssessmentResultPersonalityType> repository,
            IRepository<PersonalityType> repositoryPersonalityType,
             IRepository<UsersAssessmentResult> repositoryUsersAssessmentResult,

             IIdentityService identityService,

            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repository;
            _repositoryPersonalityType = repositoryPersonalityType;
            _repositoryUsersAssessmentResult = repositoryUsersAssessmentResult;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }



        public async Task<ReadUsersAssessmentResultPersonalityTypeDTO> Handle
            (CreateUsersAssessmentResultPersonalityTypeCommand request,
            CancellationToken cancellationToken)
        {

            var personalityType = await _repositoryPersonalityType.GetByIdAsync(request.DTO.PersonalityTypeId);
            if (personalityType == null)
                throw new NotFoundException("PersonalityType not found");

            var usersAssessmentResult = await _repositoryUsersAssessmentResult
                .GetAll()
                .Include(x => x.usersAssessment)
                .FirstOrDefaultAsync(x => x.Id == request.DTO.UsersAssessmentResultId, cancellationToken);

            if (usersAssessmentResult == null)
                throw new NotFoundException("UsersAssessmentResult not found");

            var entity = _mapper.Map<UsersAssessmentResultPersonalityType>(request.DTO);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<ReadUsersAssessmentResultPersonalityTypeDTO>(entity);

            var userId = usersAssessmentResult.usersAssessment.UserId;
            dto.UsersAssessmentResultName = await _identityService.GetFullNameAsync(userId);
            dto.PersonalityTypeName = personalityType.Name;

            return dto;
        }
    }
}
