using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands.Handlers
{
    public class CreatePersonalityTypeCommandHandler
        : IRequestHandler<CreatePersonalityTypeCommand, ReadPersonalityTypeDTO>
    {
        private readonly IRepository<PersonalityType> _repository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePersonalityTypeCommandHandler(
            IRepository<PersonalityType> repositoryAssessement,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repositoryAssessement;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }



        public async Task<ReadPersonalityTypeDTO> Handle(CreatePersonalityTypeCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<PersonalityType>(request.DTO);
            await _repository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadPersonalityTypeDTO>(result);
        }
    }
}
