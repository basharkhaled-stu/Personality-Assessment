using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Weaknesses.Queries.Handlers
{
    public class GetAllWeaknessQueryHandler :
    IRequestHandler<GetAllWeakneesQuery, PagedResult<ReadWeakneesDTO>>

    {
        private readonly IRepository<Weakness> _repository;
        private readonly IMapper _mapper;

        public GetAllWeaknessQueryHandler
            (IRepository<Weakness> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadWeakneesDTO>> Handle
            (GetAllWeakneesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Text.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<Weakness, ReadWeakneesDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
