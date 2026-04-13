using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Queries.Handlers
{
    public class GetAllUsersAssessmentResultPersonalityTypeQueryHandler :
    IRequestHandler<GetAllUsersAssessmentResultPersonalityTypeQuery, PagedResult<ReadUsersAssessmentResultPersonalityTypeDTO>>

    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetAllUsersAssessmentResultPersonalityTypeQueryHandler
            (IRepository<UsersAssessmentResultPersonalityType> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadUsersAssessmentResultPersonalityTypeDTO>> Handle(GetAllUsersAssessmentResultPersonalityTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional


            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<UsersAssessmentResultPersonalityType, ReadUsersAssessmentResultPersonalityTypeDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );


            foreach (var item in result.Items)
            {
                item.UsersAssessmentResultName = await _identityService.GetFullNameAsync(item.UsersAssessmentResultName);
            }

            return result;
        }
    }
}
