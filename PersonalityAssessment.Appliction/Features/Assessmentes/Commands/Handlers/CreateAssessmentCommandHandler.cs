using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;
using System.ComponentModel.DataAnnotations;
namespace PersonalityAssessment.Application.Features.Assessmentes.Commands.Handlers
{
    public class CreateAssessmentCommandHandler
        : IRequestHandler<CreateAssessmentCommand, ReadAssessmentDTO>
    {
        private readonly IRepository<Assessment> _repository;
        private readonly IRepository<AssessmentStatus> _repositoryStatus;
        private readonly IRepository<AssessmentType> _repositoryType;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssessmentCommandHandler(
            IRepository<Assessment> repositoryAssessement,
            IUnitOfWork unitOfWork,
            IMapper mapper,
           IRepository<AssessmentStatus> repositoryStatus,
           IRepository<AssessmentType> repositoryType
            )
        {
            _repository = repositoryAssessement;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repositoryStatus = repositoryStatus;
            _repositoryType = repositoryType;
        }





        public async Task<ReadAssessmentDTO> Handle
            (CreateAssessmentCommand request,
            CancellationToken cancellationToken)
        {
            var resultStatus = await _repositoryStatus.GetByIdAsync(request.DTO.AssessmentStatusId);
            var resultType = await _repositoryType.GetByIdAsync(request.DTO.AssessmentTypeId);
            if (resultStatus == null)
            {
                throw new ValidationException("AssessmentStatus does not exist");
            }

            if (resultType == null)
            {
                throw new ValidationException("AssessmentType does not exist");
            }

            var assessment = _mapper.Map<Assessment>(request.DTO);
            assessment.AssessmentStatus = resultStatus;
            assessment.AssessmentType = resultType;

            await _repository.AddAsync(assessment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReadAssessmentDTO>(assessment);

        }
    }
}
