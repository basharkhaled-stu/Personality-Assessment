using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands.Handlers
{
    public class CreateUserAssessmentStatusCommandHandler
        : IRequestHandler<CreateUserAssessmentStatusCommand, ReadUserAssessmentStatusDTO>
    {
        private readonly IRepository<UserAssessmentStatus> _repository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserAssessmentStatusCommandHandler(
            IRepository<UserAssessmentStatus> repositoryAssessement,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repositoryAssessement;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }



        public async Task<ReadUserAssessmentStatusDTO> Handle(CreateUserAssessmentStatusCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<UserAssessmentStatus>(request.DTO);
            await _repository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadUserAssessmentStatusDTO>(result);
        }
    }
}
