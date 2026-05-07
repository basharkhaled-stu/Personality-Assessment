using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands.Handlers
{
    public class DeletePersonalityTypeCommandHandler
        : IRequestHandler<DeletePersonalityTypeCommand, bool>
    {
        private readonly IRepository<PersonalityType> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonalityTypeCommandHandler(
            IRepository<PersonalityType> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle
            (DeletePersonalityTypeCommand request,
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
