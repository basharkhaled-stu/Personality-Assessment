using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands.Handlers
{
    public class DeleteUserAssessmentStatusCommandHandler
        : IRequestHandler<DeleteUserAssessmentStatusCommand, bool>
    {
        private readonly IRepository<UserAssessmentStatus> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserAssessmentStatusCommandHandler(
            IRepository<UserAssessmentStatus> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeleteUserAssessmentStatusCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.id);

            if (result == null)
            {
                return false;
            }
            result.IsDeleted = true;
            result.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
