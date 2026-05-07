using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries.Handlers
{
    public class GetByIDAdmainUsersAssessmentQueryHandler
         : IRequestHandler<GetUsersAssessmentByIdAdmainQuery, AdmainReadUsersAssessmentDTO>
    {
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IRepository<UserAssessmentStatus> _repositoryUserAssessmentStatus;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetByIDAdmainUsersAssessmentQueryHandler(
            IRepository<UsersAssessment> repository,
            IRepository<UserAssessmentStatus> repositoryUserAssessmentStatus,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUserAssessmentStatus = repositoryUserAssessmentStatus;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<AdmainReadUsersAssessmentDTO> Handle(
            GetUsersAssessmentByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
                throw new NotFoundException($"UsersAssessment with ID {request.id} not found.");

            var statusName = "";
            try
            {
                var status = await _repositoryUserAssessmentStatus.GetByIdAsync(entity.UserAssessmentStatusId);
                statusName = status?.Name ?? "";
            }
            catch { }

            var userName = "";
            try
            {
                userName = await _identityService.GetFullNameAsync(entity.UserId) ?? entity.UserId;
            }
            catch { }

            return new AdmainReadUsersAssessmentDTO
            {
                Id = entity.Id,
                UserName = userName,
                UserAssessmentStatusName = statusName,
                StartedAt = entity.StartedAt,
                CompletedAt = entity.CompletedAt,
                CreatedAt = entity.CreatedAt,
                CreatedByUserId = entity.CreatedByUserId,
                UpdatedAt = entity.UpdatedAt,
                UpdatedByUserId = entity.UpdatedByUserId
            };
        }
    }
}
