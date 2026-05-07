using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class CreateOptionCommandHandler
        : IRequestHandler<CreateOptionCommand, ReadOptionDTO>
    {
        private readonly IRepository<Option> _repository;
        private readonly IRepository<Question> _repositoryQuestion;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOptionCommandHandler(
            IRepository<Option> repository,
            IRepository<Question> repositoryQuestion,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repositoryQuestion = repositoryQuestion;

        }



        public async Task<ReadOptionDTO> Handle
            (CreateOptionCommand request,
            CancellationToken cancellationToken)
        {

            var isfound = await _repositoryQuestion.GetByIdAsync(request.DTO.QuestionId);

            if (isfound == null)
            {
                throw new Exception("Not Found");
            }

            int max = _repository.GetAll()
                 .Where(a => a.QuestionId == request.DTO.QuestionId)
                 .Select(a => (int?)a.DisplayOrder)
                   .Max() ?? 0;

            var result = _mapper.Map<Option>(request.DTO);
            result.Question = isfound;


            result.DisplayOrder = max + 1;
            await _repository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadOptionDTO>(result);
        }
    }
}
