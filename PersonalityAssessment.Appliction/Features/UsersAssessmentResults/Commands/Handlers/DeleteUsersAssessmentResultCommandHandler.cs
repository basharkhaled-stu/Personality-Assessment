using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands.Handlers
{
    public class DeleteUsersAssessmentResultCommandHandler
        : IRequestHandler<DeleteUsersAssessmentResultCommand, bool>
    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsersAssessmentResultCommandHandler(
            IRepository<UsersAssessmentResult> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteUsersAssessmentResultCommand request, CancellationToken cancellationToken)
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
