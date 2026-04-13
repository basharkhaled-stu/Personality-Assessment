using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands.Handlers
{
    public class UpdateWeakneesCommandHandler :
        IRequestHandler<UpdateWeaknessCommand, bool>
    {
        private readonly IRepository<Weakness> _repository;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWeakneesCommandHandler
        (IRepository<Weakness> repository,
         IRepository<PersonalityType> repositoryPersonalityType,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryPersonalityType = repositoryPersonalityType;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateWeaknessCommand request, CancellationToken cancellationToken)
        {


            var resultPersonalityType = await _repositoryPersonalityType.GetByIdAsync(request.dto.PersonalityTypeId);
            if (resultPersonalityType == null)
            {
                throw new NotFoundException("PersonalityType Not Found");
            }


            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                return false;
            }

            _mapper.Map(request.dto, result);
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int Key = await _unitOfWork.SaveChangesAsync();

            return Key > 0;
        }
    }
}
