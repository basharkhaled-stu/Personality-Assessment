using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands.Handlers
{
    public class DeleteWeaknessCommandHandler
        : IRequestHandler<DeleteWeaknessCommand, bool>
    {
        private readonly IRepository<Weakness> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWeaknessCommandHandler(
            IRepository<Weakness> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteWeaknessCommand request, CancellationToken cancellationToken)
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
