using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands.Handlers
{
    public class CreateAssessmentStatusCommandHandler
        : IRequestHandler<CreateAssessmentTypeCommand, ReadAssessmentTypeDTO>
    {
        private readonly IRepository<AssessmentType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssessmentStatusCommandHandler(
            IRepository<AssessmentType> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<ReadAssessmentTypeDTO> Handle(CreateAssessmentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AssessmentType>(request.DTO);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadAssessmentTypeDTO>(entity);
        }
    }
}
