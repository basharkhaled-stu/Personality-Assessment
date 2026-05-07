using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Queries.Handlers
{
    public class GetByIDQuestionTypeQueryHandler :
        IRequestHandler<GetQuestionTypeByIdQuery, ReadQuestionTypeDTO>
    {
        private readonly IRepository<QuestionType> _repository;

        private readonly IMapper _mapper;

        public GetByIDQuestionTypeQueryHandler(
            IRepository<QuestionType> repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public async Task<ReadQuestionTypeDTO> Handle(GetQuestionTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null)
            {
                throw new NotFoundException($"QuestionType with ID {request.id} not found.");
            }
            return _mapper.Map<ReadQuestionTypeDTO>(entity);
        }
    }
}
