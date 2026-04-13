using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Questions.Commands.Handlers
{
    public class DeleteQuestionCommandHandler
        : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionCommandHandler(
            IRepository<UsersAssessmentResultPersonalityType> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeleteQuestionCommand request,
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
