using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries.Handlers
{
    public class GetAllUsersAssessmentResultQueryHandler :
    IRequestHandler<GetAllUsersAssessmentResultQuery, PagedResult<ReadUsersAssessmentResultDTO>>

    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetAllUsersAssessmentResultQueryHandler
            (IRepository<UsersAssessmentResult> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadUsersAssessmentResultDTO>> Handle
            (GetAllUsersAssessmentResultQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<UsersAssessmentResult, ReadUsersAssessmentResultDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            foreach (var item in result.Items)
            {
                item.UsersAssessmentName = await _identityService.GetFullNameAsync(item.UsersAssessmentName);
            }

            return result;
        }
    }
}
