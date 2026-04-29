using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Handlers
{
    public class CreateUsersAssessmentCommandHandler
        : IRequestHandler<CreateUsersAssessmentCommand, ReadUsersAssessmentDTO>
    {
        private readonly IRepository<UserAssessmentStatus> _repositoryUserAssessmentStatus;
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUsersAssessmentCommandHandler(
            IRepository<UsersAssessment> repository,
            IRepository<UserAssessmentStatus> repositoryUserAssessmentStatus,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUserAssessmentStatus = repositoryUserAssessmentStatus;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadUsersAssessmentDTO> Handle(
            CreateUsersAssessmentCommand request,
            CancellationToken cancellationToken)
        {
            var status = await _repositoryUserAssessmentStatus.GetByIdAsync(request.DTO.UserAssessmentStatusId);
            if (status == null)
                throw new NotFoundException("UserAssessmentStatus not found");

            var result = _mapper.Map<UsersAssessment>(request.DTO);
            result.UserId = request.UserId;
            result.StartedAt = DateTime.UtcNow;
            result.CreatedAt = DateTime.UtcNow;

            // Assign navigation property so mapper won't throw NullReferenceException
            result.UserAssessmentStatus = status;

            await _repository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadUsersAssessmentDTO>(result);
        }
    }
}
