using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries.Handlers
{
    public class GetByIDUsersAssessmentResultQueryHandler :
        IRequestHandler<GetUsersAssessmentResultByIdQuery, ReadUsersAssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetByIDUsersAssessmentResultQueryHandler(
            IRepository<UsersAssessmentResult> repository,
            IRepository<UsersAssessment> repositoryUsersAssessment,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ReadUsersAssessmentResultDTO> Handle(GetUsersAssessmentResultByIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<ReadUsersAssessmentResultDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"UsersAssessmentResult with ID {request.id} not found.");

            // BUG FIX: get the actual UserId from UsersAssessment, then resolve full name
            try
            {
                var ua = await _repositoryUsersAssessment.GetByIdAsync(dto.UsersAssessmentId);
                if (ua != null)
                    dto.UsersAssessmentName = await _identityService.GetFullNameAsync(ua.UserId) ?? "";
            }
            catch { dto.UsersAssessmentName = ""; }

            return dto;
        }
    }
}
