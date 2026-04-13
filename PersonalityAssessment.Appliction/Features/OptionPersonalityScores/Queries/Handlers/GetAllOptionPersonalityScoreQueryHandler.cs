using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Queries.Handlers
{
    public class GetAllOptionPersonalityScoreQueryHandler :
    IRequestHandler<GetAllOptionPersonalityScoreQuery, PagedResult<ReadOptionPersonalityScoreDTO>>

    {
        private readonly IRepository<OptionPersonalityScore> _repository;
        private readonly IMapper _mapper;

        public GetAllOptionPersonalityScoreQueryHandler
            (IRepository<OptionPersonalityScore> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadOptionPersonalityScoreDTO>> Handle(GetAllOptionPersonalityScoreQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional


            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<OptionPersonalityScore, ReadOptionPersonalityScoreDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
