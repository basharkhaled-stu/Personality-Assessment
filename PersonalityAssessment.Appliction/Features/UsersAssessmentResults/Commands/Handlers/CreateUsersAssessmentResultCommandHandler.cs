using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands.Handlers
{
    public class CreateUsersAssessmentResultCommandHandler
        : IRequestHandler<CreateUsersAssessmentResultCommand, ReadUsersAssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IIdentityService _identityService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUsersAssessmentResultCommandHandler(
            IRepository<UsersAssessmentResult> repository,
            IRepository<UsersAssessment> repositoryUsersAssessment,
            IIdentityService identityService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<ReadUsersAssessmentResultDTO> Handle
            (CreateUsersAssessmentResultCommand request,
            CancellationToken cancellationToken)
        {
            var resultUsersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.DTO.UsersAssessmentId);
            if (resultUsersAssessment == null)
            {
                throw new NotFoundException("UsersAssessment Not Found");
            }

            var entity = _mapper.Map<UsersAssessmentResult>(request.DTO);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<ReadUsersAssessmentResultDTO>(entity);
            dto.UsersAssessmentName = await _identityService.GetFullNameAsync(dto.UsersAssessmentName);
            return dto;
        }
    }
}
