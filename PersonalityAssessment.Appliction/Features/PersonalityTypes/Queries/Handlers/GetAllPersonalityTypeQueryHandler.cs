using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries.Handlers
{
    public class GetAllPersonalityTypeQueryHandler :
    IRequestHandler<GetAllPersonalityTypeQuery, PagedResult<ReadPersonalityTypeDTO>>

    {
        private readonly IRepository<PersonalityType> _repository;
        private readonly IMapper _mapper;

        public GetAllPersonalityTypeQueryHandler
            (IRepository<PersonalityType> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadPersonalityTypeDTO>> Handle(GetAllPersonalityTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Name.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<PersonalityType, ReadPersonalityTypeDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
