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
    public class GetByIDOptionPersonalityScoreQueryHandler :
        IRequestHandler<GetOptionPersonalityScoreByIdQuery, ReadOptionPersonalityScoreDTO>
    {
        private readonly IRepository<OptionPersonalityScore> _repository;

        private readonly IMapper _mapper;

        public GetByIDOptionPersonalityScoreQueryHandler(
            IRepository<OptionPersonalityScore> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadOptionPersonalityScoreDTO> Handle
            (GetOptionPersonalityScoreByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadOptionPersonalityScoreDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"OptionPersonalityScore with ID {request.id} not found.");

            return _mapper.Map<ReadOptionPersonalityScoreDTO>(dto);
        }
    }
}
