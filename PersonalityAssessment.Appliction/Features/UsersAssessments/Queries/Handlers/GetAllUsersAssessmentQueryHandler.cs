using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries.Handlers
{
    public class GetAllUsersAssessmentQueryHandler :
    IRequestHandler<GetAllUsersAssessmentQuery, PagedResult<ReadUsersAssessmentDTO>>

    {
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IIdentityService _identityService;

        private readonly IMapper _mapper;

        public GetAllUsersAssessmentQueryHandler
            (IRepository<UsersAssessment> repository,
              IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadUsersAssessmentDTO>> Handle(GetAllUsersAssessmentQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.UserId.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<UsersAssessment, ReadUsersAssessmentDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );
            foreach (var item in result.Items)
            {
                item.UserName = await _identityService.GetFullNameAsync(item.UserName);
            }

            return result;
        }
    }
}
