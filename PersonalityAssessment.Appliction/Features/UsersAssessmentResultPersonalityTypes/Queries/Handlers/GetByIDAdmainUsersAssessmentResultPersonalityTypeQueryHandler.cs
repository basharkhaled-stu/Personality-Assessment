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
    public class GetByIDAdmainUsersAssessmentResultPersonalityTypeQueryHandler
         : IRequestHandler<GetUsersAssessmentResultPersonalityTypeByIdAdmainQuery, AdmainReadUsersAssessmentResultPersonalityTypeDTO>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IIdentityService _identityService;

        private readonly IMapper _mapper;

        public GetByIDAdmainUsersAssessmentResultPersonalityTypeQueryHandler
            (IRepository<UsersAssessmentResultPersonalityType> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<AdmainReadUsersAssessmentResultPersonalityTypeDTO> Handle
            (GetUsersAssessmentResultPersonalityTypeByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadUsersAssessmentResultPersonalityTypeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (dto == null)
            {
                throw new NotFoundException("Not Found UsersAssessmentResultPersonalityType");
            }

            dto.UsersAssessmentResultName = await _identityService.GetFullNameAsync(dto.UsersAssessmentResultName);
            return dto;
        }
    }



}
