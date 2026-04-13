using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands.Handlers
{
    public class DeleteUsersAssessmentResultPersonalityTypeCommandHandler
        : IRequestHandler<DeleteUsersAssessmentResultPersonalityTypeCommand, bool>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsersAssessmentResultPersonalityTypeCommandHandler(
            IRepository<UsersAssessmentResultPersonalityType> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeleteUsersAssessmentResultPersonalityTypeCommand request,
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
