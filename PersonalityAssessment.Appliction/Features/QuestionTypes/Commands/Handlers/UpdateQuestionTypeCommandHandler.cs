using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands.Handlers
{
    public class UpdateQuestionTypeCommandHandler :
        IRequestHandler<UpdateQuestionTypeCommand, bool>
    {
        private readonly IRepository<QuestionType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateQuestionTypeCommandHandler
        (IRepository<QuestionType> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle
            (UpdateQuestionTypeCommand request,
            CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                throw new NotFoundException("QuestionType not found");
            }

            _mapper.Map(request.dto, result);
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int Key = await _unitOfWork.SaveChangesAsync();

            return Key > 0;
        }
    }
}
