using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Queries.Handlers
{
    public class GetByIDAssessmentTypeQueryHandler :
        IRequestHandler<GetAssessmentTypeByIdQuery, ReadAssessmentTypeDTO>
    {
        private readonly IRepository<AssessmentType> _repository;

        private readonly IMapper _mapper;

        public GetByIDAssessmentTypeQueryHandler(
            IRepository<AssessmentType> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadAssessmentTypeDTO> Handle(GetAssessmentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<ReadAssessmentTypeDTO>(entity);
        }
    }
}
