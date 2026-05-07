using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands.Handlers
{
    public class DeleteAssessmentTypeCommandHandler
        : IRequestHandler<DeleteAssessmentTypeCommand, bool>
    {
        private readonly IRepository<AssessmentType> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssessmentTypeCommandHandler(
            IRepository<AssessmentType> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteAssessmentTypeCommand request, CancellationToken cancellationToken)
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
