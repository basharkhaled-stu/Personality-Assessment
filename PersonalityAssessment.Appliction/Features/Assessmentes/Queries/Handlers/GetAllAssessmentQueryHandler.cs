using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Assessmentes.Queries.Handlers
{
    public class GetAllAssessmentQueryHandler :
    IRequestHandler<GetAllAssessmentQuery, PagedResult<ReadAssessmentDTO>>

    {
        private readonly IRepository<Assessment> _repository;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "Assessmentes:all";
        private readonly IMapper _mapper;

        public GetAllAssessmentQueryHandler
            (IRepository<Assessment> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<PagedResult<ReadAssessmentDTO>> Handle(GetAllAssessmentQuery request, CancellationToken cancellationToken)
        {

            var cachedData = await _cacheService.GetAsync<PagedResult<ReadAssessmentDTO>>(CacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            await _cacheService.SetAsync(CacheKey, cachedData, TimeSpan.FromMinutes(10));

            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Title.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<Assessment, ReadAssessmentDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
