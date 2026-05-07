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

        public GetAllAssessmentQueryHandler(
            IRepository<Assessment> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PagedResult<ReadAssessmentDTO>> Handle(GetAllAssessmentQuery request, CancellationToken cancellationToken)
        {
            // BUG FIX: only return cache if it's not null, and set cache AFTER fetching
            var cachedData = await _cacheService.GetAsync<PagedResult<ReadAssessmentDTO>>(CacheKey);
            if (cachedData != null)
                return cachedData;

            var query = _repository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.p.Search))
                query = query.Where(x => x.Title.Contains(request.p.Search));

            var result = await query.ToPagedResultAsync<Assessment, ReadAssessmentDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            // BUG FIX: set cache AFTER result is fetched (was setting null before)
            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }
    }
}
