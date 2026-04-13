using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries.Handlers
{
    public class GetByIDAdmainUserAssessmentStatusQueryHandler
         : IRequestHandler<GetUserAssessmentStatusByIdAdmainQuery, AdmainUserAssessmentStatusDTO>
    {
        private readonly IRepository<UserAssessmentStatus> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainUserAssessmentStatusQueryHandler
            (IRepository<UserAssessmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainUserAssessmentStatusDTO> Handle
            (GetUserAssessmentStatusByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<AdmainUserAssessmentStatusDTO>(entity);
        }
    }



}
