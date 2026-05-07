using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands.Handlers
{
    public class UpdateAssessmentStatusesCommandHandler :
        IRequestHandler<UpdateAssessmentStatusCommand, ReadAssessmentStatusDTO>
    {
        private readonly IRepository<AssessmentStatus> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAssessmentStatusesCommandHandler
        (IRepository<AssessmentStatus> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReadAssessmentStatusDTO> Handle(UpdateAssessmentStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.id != request.dto.Id)
            {
                return null;
            }
            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                return null;
            }

            _mapper.Map(request.dto, result);
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadAssessmentStatusDTO>(result);
        }
    }
}
