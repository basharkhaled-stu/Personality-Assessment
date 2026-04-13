using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands.Handlers
{
    public class CreateQuestionTypeCommandHandler
        : IRequestHandler<CreateQuestionTypeCommand, ReadQuestionTypeDTO>
    {
        private readonly IRepository<QuestionType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateQuestionTypeCommandHandler(
            IRepository<QuestionType> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<ReadQuestionTypeDTO> Handle
            (CreateQuestionTypeCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<QuestionType>(request.DTO);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadQuestionTypeDTO>(entity);
        }
    }
}
