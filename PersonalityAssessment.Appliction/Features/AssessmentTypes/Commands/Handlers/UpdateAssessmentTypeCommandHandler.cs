using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands.Handlers
{
    public class UpdateAssessmentTypeCommandHandler :
        IRequestHandler<UpdateAssessmentTypeCommand, ReadAssessmentTypeDTO>
    {
        private readonly IRepository<AssessmentType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAssessmentTypeCommandHandler
        (IRepository<AssessmentType> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReadAssessmentTypeDTO> Handle(UpdateAssessmentTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.id != request.dto.Id)
            {
                return null;
            }
            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                return null;
            }

            _mapper.Map(request.dto, result);
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadAssessmentTypeDTO>(result);
        }
    }
}
