using MediatR;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands.Handlers
{
    public class DeleteQuestionTypeCommandHandler
        : IRequestHandler<DeleteQuestionTypeCommand, bool>
    {
        private readonly IRepository<QuestionType> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionTypeCommandHandler(
            IRepository<QuestionType> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteQuestionTypeCommand request, CancellationToken cancellationToken)
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
