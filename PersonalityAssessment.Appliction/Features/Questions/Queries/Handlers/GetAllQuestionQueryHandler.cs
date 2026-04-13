using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Questions.Queries.Handlers
{
    public class GetAllQuestionQueryHandler :
    IRequestHandler<GetAllQuestionQuery, PagedResult<ReadQuestionDTO>>

    {
        private readonly IRepository<Question> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private string CacheKey = "Questiones:all";

        public GetAllQuestionQueryHandler
            (IRepository<Question> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadQuestionDTO>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
        {

            var cachData = await _cacheService.GetAsync<PagedResult<ReadQuestionDTO>>(CacheKey);

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
            var result = await query.ToPagedResultAsync<Question, ReadQuestionDTO>(
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
