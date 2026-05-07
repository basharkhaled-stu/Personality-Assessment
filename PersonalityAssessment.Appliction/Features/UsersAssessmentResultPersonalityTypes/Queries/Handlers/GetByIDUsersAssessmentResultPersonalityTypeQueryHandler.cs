using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Queries.Handlers
{
    public class GetByIDUsersAssessmentResultPersonalityTypeQueryHandler :
        IRequestHandler<GetUsersAssessmentResultPersonalityTypeByIdQuery, ReadUsersAssessmentResultPersonalityTypeDTO>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetByIDUsersAssessmentResultPersonalityTypeQueryHandler(
            IRepository<UsersAssessmentResultPersonalityType> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<ReadUsersAssessmentResultPersonalityTypeDTO> Handle
            (GetUsersAssessmentResultPersonalityTypeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadUsersAssessmentResultPersonalityTypeDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"UsersAssessmentResultPersonalityType with ID {request.id} not found.");

            dto.UsersAssessmentResultName = await _identityService.GetFullNameAsync(dto.UsersAssessmentResultName);
            return dto;
        }
    }
}
