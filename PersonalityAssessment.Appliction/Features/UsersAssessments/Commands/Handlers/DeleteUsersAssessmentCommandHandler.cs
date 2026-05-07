using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Handlers
{
    public class DeleteUsersAssessmentCommandHandler
        : IRequestHandler<DeleteUsersAssessmentCommand, bool>
    {
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsersAssessmentCommandHandler(
            IRepository<UsersAssessment> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeleteUsersAssessmentCommand request,
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
