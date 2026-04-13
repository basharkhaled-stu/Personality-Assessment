using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Queries.Handlers
{
    public class GetByIDAdmainOptionPersonalityScoreQueryHandler
         : IRequestHandler<GetOptionPersonalityScoreByIdAdmainQuery, AdmainReadOptionPersonalityScoreDTO>
    {
        private readonly IRepository<OptionPersonalityScore> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainOptionPersonalityScoreQueryHandler
            (IRepository<OptionPersonalityScore> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainReadOptionPersonalityScoreDTO> Handle
            (GetOptionPersonalityScoreByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                  .Where(a => a.Id == request.id)
                  .ProjectTo<AdmainReadOptionPersonalityScoreDTO>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"OptionPersonalityScore with ID {request.id} not found.");

            return _mapper.Map<AdmainReadOptionPersonalityScoreDTO>(dto);
        }
    }



}
