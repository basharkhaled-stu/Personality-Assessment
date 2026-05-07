using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Queries.Handlers
{
    public class GetAllAssessmentStatusesQueryHandler :
    IRequestHandler<GetAllAssessmentStatusQuery, PagedResult<ReadAssessmentStatusDTO>>

    {
        private readonly IRepository<AssessmentStatus> _repository;
        private readonly IMapper _mapper;

        public GetAllAssessmentStatusesQueryHandler
            (IRepository<AssessmentStatus> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadAssessmentStatusDTO>> Handle(GetAllAssessmentStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Name.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<AssessmentStatus, ReadAssessmentStatusDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
