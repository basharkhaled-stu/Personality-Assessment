using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries.Handlers
{
    public class GetByIDPersonalityTypeQueryHandler :
        IRequestHandler<GetPersonalityTypeByIdQuery, ReadPersonalityTypeDTO>
    {
        private readonly IRepository<PersonalityType> _repository;

        private readonly IMapper _mapper;

        public GetByIDPersonalityTypeQueryHandler(
            IRepository<PersonalityType> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadPersonalityTypeDTO> Handle
            (GetPersonalityTypeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadPersonalityTypeDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Assessment with ID {request.id} not found.");

            return _mapper.Map<ReadPersonalityTypeDTO>(dto);
        }
    }
}
