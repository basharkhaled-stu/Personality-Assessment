using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands.Handlers
{
    public class DeleteAssessmentStatusesCommandHandler
        : IRequestHandler<DeleteAssessmentStatusCommand, bool>
    {
        private readonly IRepository<AssessmentStatus> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssessmentStatusesCommandHandler(
            IRepository<AssessmentStatus> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteAssessmentStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);

            if (entity is null)
                return false;


            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}
