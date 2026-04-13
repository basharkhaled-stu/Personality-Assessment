using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands.Handlers
{
    public class CreateAssessmentStatusCommandHandler
        : IRequestHandler<CreateWeaknessCommand, ReadWeakneesDTO>
    {
        private readonly IRepository<Weakness> _repository;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssessmentStatusCommandHandler(
            IRepository<Weakness> repository,
            IRepository<PersonalityType> repositoryPersonalityType,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryPersonalityType = repositoryPersonalityType;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<ReadWeakneesDTO> Handle(CreateWeaknessCommand request, CancellationToken cancellationToken)
        {

            var resultPersonalityType = await _repositoryPersonalityType.GetByIdAsync(request.DTO.PersonalityTypeId);
            if (resultPersonalityType == null)
            {
                throw new NotFoundException("PersonalityType Not Found");
            }

            var entity = _mapper.Map<Weakness>(request.DTO);
            entity.PersonalityType = resultPersonalityType;
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadWeakneesDTO>(entity);
        }
    }
}
