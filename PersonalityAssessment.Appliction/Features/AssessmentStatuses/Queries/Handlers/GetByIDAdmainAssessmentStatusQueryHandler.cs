using AutoMapper;
using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Queries.Handlers
{
    public class GetByIDAdmainAssessmentStatusQueryHandler
         : IRequestHandler<GetAssessmentStatusesByIdAdmainQuery, AdmainReadAssessmentStatusDTO>
    {
        private readonly IRepository<AssessmentStatus> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainAssessmentStatusQueryHandler
            (IRepository<AssessmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainReadAssessmentStatusDTO> Handle(GetAssessmentStatusesByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<AdmainReadAssessmentStatusDTO>(entity);
        }
    }



}
