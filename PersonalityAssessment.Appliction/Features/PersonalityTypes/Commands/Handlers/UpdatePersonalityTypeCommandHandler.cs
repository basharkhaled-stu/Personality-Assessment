using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;
namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands.Handlers
{
    public class UpdatePersonalityTypeCommandHandler :
        IRequestHandler<UpdatePersonalityTypeCommand, bool>
    {
        private readonly IRepository<PersonalityType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePersonalityTypeCommandHandler
        (IRepository<PersonalityType> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle
            (UpdatePersonalityTypeCommand request,
            CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                throw new NotFoundException("PersonalityType not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
