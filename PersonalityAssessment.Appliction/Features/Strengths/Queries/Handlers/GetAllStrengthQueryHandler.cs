using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Strengths.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Strengths.Queries.Handlers
{
    public class GetAllStrengthQueryHandler :
    IRequestHandler<GetAllStrengthQuery, PagedResult<ReadStrengthDTO>>

    {
        private readonly IRepository<Strength> _repository;
        private readonly IMapper _mapper;

        public GetAllStrengthQueryHandler
            (IRepository<Strength> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadStrengthDTO>> Handle
            (GetAllStrengthQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Text.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<Strength, ReadStrengthDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
