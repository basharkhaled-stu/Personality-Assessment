using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries.Handlers
{
    public class GetByIDAdmainUserAssessmentStatusQueryHandler : IRequestHandler<GetUserAssessmentStatusByIdAdmainQuery, AdmainUserAssessmentStatusDTO>
    {
        private readonly IRepository<UserAssessmentStatus> _repository;
        private readonly IMapper _mapper;
        public GetByIDAdmainUserAssessmentStatusQueryHandler(IRepository<UserAssessmentStatus> repository, IMapper mapper)
        { _repository = repository; _mapper = mapper; }
        public async Task<AdmainUserAssessmentStatusDTO> Handle(GetUserAssessmentStatusByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null) throw new NotFoundException($"UserAssessmentStatus with ID {request.id} not found.");
            return _mapper.Map<AdmainUserAssessmentStatusDTO>(entity);
        }
    }
}
