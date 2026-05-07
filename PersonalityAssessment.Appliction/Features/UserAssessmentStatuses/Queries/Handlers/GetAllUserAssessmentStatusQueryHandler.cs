using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries.Handlers
{
    public class GetAllUserAssessmentStatusQueryHandler :
    IRequestHandler<GetAllUserAssessmentStatusQuery, PagedResult<ReadUserAssessmentStatusDTO>>

    {
        private readonly IRepository<UserAssessmentStatus> _repository;
        private readonly IMapper _mapper;

        public GetAllUserAssessmentStatusQueryHandler
            (IRepository<UserAssessmentStatus> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadUserAssessmentStatusDTO>> Handle(GetAllUserAssessmentStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Name.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<UserAssessmentStatus, ReadUserAssessmentStatusDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
