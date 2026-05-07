using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Strengths.Commands.Handlers
{
    public class DeleteStrengthCommandHandler
        : IRequestHandler<DeleteStrengthCommand, bool>
    {
        private readonly IRepository<Strength> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStrengthCommandHandler(
            IRepository<Strength> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteStrengthCommand request, CancellationToken cancellationToken)
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
