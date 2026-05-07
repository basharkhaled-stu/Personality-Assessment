using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Weaknesses.Queries.Handlers
{
    public class GetByIDAdmainWeakneesQueryHandler : IRequestHandler<GetWeakneesByIdAdmainQuery, AdmainReadWeaknessDTO>
    {
        private readonly IRepository<Weakness> _repository;
        private readonly IMapper _mapper;
        public GetByIDAdmainWeakneesQueryHandler(IRepository<Weakness> repository, IMapper mapper)
        { _repository = repository; _mapper = mapper; }

        public async Task<AdmainReadWeaknessDTO> Handle(GetWeakneesByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll().Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadWeaknessDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (dto == null) throw new NotFoundException($"Weakness with ID {request.id} not found.");
            return dto; // BUG FIX: removed double mapping
        }
    }
}
