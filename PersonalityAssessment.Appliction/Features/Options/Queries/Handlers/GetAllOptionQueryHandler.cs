using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetAllOptionQueryHandler :
    IRequestHandler<GetAllOptionQuery, PagedResult<ReadOptionDTO>>

    {
        private readonly IRepository<Option> _repository;
        private readonly ICacheService _cacheService;
        private readonly string CacheKey = "Option:all";
        private readonly IMapper _mapper;

        public GetAllOptionQueryHandler
            (IRepository<Option> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadOptionDTO>> Handle(GetAllOptionQuery request, CancellationToken cancellationToken)
        {
            var cachData = await _cacheService.GetAsync<PagedResult<ReadOptionDTO>>(CacheKey);

            if (cachData != null)
            {
                return cachData;
            }



            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Text.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<Option, ReadOptionDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );


            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(60));
            return result;
        }
    }
}
