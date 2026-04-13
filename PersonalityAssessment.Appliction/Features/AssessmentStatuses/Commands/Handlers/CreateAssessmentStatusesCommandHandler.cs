using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands.Handlers
{
    public class CreateAssessmentStatusCommandHandler
        : IRequestHandler<CreateAssessmentStatusCommand, ReadAssessmentStatusDTO>
    {
        private readonly IRepository<AssessmentStatus> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssessmentStatusCommandHandler(
            IRepository<AssessmentStatus> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<ReadAssessmentStatusDTO> Handle(CreateAssessmentStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AssessmentStatus>(request.DTO);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadAssessmentStatusDTO>(entity);
        }
    }
}
