using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Queries.Handlers
{
    public class GetByIDAdmainAssessmentTypeQueryHandler : IRequestHandler<GetAssessmentTypeByIdAdmainQuery, AdmainReadAssessmentTypeDTO>
    {
        private readonly IRepository<AssessmentType> _repository;
        private readonly IMapper _mapper;
        public GetByIDAdmainAssessmentTypeQueryHandler(IRepository<AssessmentType> repository, IMapper mapper)
        { _repository = repository; _mapper = mapper; }
        public async Task<AdmainReadAssessmentTypeDTO> Handle(GetAssessmentTypeByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null) throw new NotFoundException($"AssessmentType with ID {request.id} not found.");
            return _mapper.Map<AdmainReadAssessmentTypeDTO>(entity);
        }
    }
}
