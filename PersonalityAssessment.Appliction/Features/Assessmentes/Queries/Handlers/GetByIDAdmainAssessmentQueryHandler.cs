using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Assessmentes.Queries.Handlers
{
    public class GetByIDAdmainAssessmentQueryHandler
        : IRequestHandler<GetAssessmentByIdAdmainQuery, AdmainReadAssessmentDTO>
    {
        private readonly IRepository<Assessment> _repository;
        private readonly IMapper _mapper;

        public GetByIDAdmainAssessmentQueryHandler(IRepository<Assessment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AdmainReadAssessmentDTO> Handle(GetAssessmentByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            // BUG FIX: removed double mapping + use NotFoundException instead of Exception
            var dto = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadAssessmentDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Assessment with ID {request.id} not found.");

            return dto;
        }
    }
}
