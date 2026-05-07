using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands.Handlers
{
    public class DeletePersonalityScoreCommandHandler
        : IRequestHandler<DeleteOptionPersonalityScoreCommand, bool>
    {
        private readonly IRepository<OptionPersonalityScore> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonalityScoreCommandHandler(
            IRepository<OptionPersonalityScore> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeleteOptionPersonalityScoreCommand request,
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
