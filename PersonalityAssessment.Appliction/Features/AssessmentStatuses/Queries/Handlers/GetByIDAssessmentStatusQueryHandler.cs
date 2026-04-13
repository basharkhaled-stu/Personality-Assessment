using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Queries.Handlers
{
    public class GetByIDAssessmentStatusQueryHandler :
        IRequestHandler<GetAssessmentStatusByIdQuery, ReadAssessmentStatusDTO>
    {
        private readonly IRepository<AssessmentStatus> _repository;

        private readonly IMapper _mapper;

        public GetByIDAssessmentStatusQueryHandler(
            IRepository<AssessmentStatus> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadAssessmentStatusDTO> Handle(GetAssessmentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<ReadAssessmentStatusDTO>(entity);
        }
    }
}
