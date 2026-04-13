using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries.Handlers
{
    public class GetByIDUserAssessmentStatusQueryHandler :
        IRequestHandler<GetUserAssessmentStatusByIdQuery, ReadUserAssessmentStatusDTO>
    {
        private readonly IRepository<UserAssessmentStatus> _repository;

        private readonly IMapper _mapper;

        public GetByIDUserAssessmentStatusQueryHandler(
            IRepository<UserAssessmentStatus> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadUserAssessmentStatusDTO> Handle
            (GetUserAssessmentStatusByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);

            if (entity == null)
                throw new NotFoundException($"Assessment with ID {request.id} not found.");

            return _mapper.Map<ReadUserAssessmentStatusDTO>(entity);
        }
    }
}
