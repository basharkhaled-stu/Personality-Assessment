using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetByIDAdmainOptionQueryHandler
        : IRequestHandler<GetOptionByIdAdmainQuery, AdmainReadOptionDTO>
    {
        private readonly IRepository<Option> _repository;
        private readonly IMapper _mapper;

        public GetByIDAdmainOptionQueryHandler(IRepository<Option> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AdmainReadOptionDTO> Handle(GetOptionByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            // BUG FIX: removed double mapping + use NotFoundException
            var dto = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadOptionDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Option with ID {request.id} not found.");

            return dto;
        }
    }
}
