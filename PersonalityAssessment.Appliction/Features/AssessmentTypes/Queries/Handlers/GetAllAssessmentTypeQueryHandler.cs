using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Queries.Handlers
{
    public class GetAllAssessmentTypeQueryHandler :
    IRequestHandler<GetAllAssessmentTypeQuery, PagedResult<ReadAssessmentTypeDTO>>

    {
        private readonly IRepository<AssessmentType> _repository;
        private readonly IMapper _mapper;

        public GetAllAssessmentTypeQueryHandler
            (IRepository<AssessmentType> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadAssessmentTypeDTO>> Handle
            (GetAllAssessmentTypeQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Name.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<AssessmentType, ReadAssessmentTypeDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
