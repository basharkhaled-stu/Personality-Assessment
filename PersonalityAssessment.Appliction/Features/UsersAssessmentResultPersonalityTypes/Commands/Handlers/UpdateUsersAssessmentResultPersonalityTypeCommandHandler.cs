using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands.Handlers
{
    public class UpdateUsersAssessmentResultPersonalityTypeCommandHandler :
        IRequestHandler<UpdateUsersAssessmentResultPersonalityTypeCommand, bool>
    {
        private readonly IRepository<UsersAssessmentResultPersonalityType> _repository;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IRepository<UsersAssessmentResult> _repositoryUsersAssessmentResult;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;



        public UpdateUsersAssessmentResultPersonalityTypeCommandHandler(
            IRepository<UsersAssessmentResultPersonalityType> repository,
            IRepository<PersonalityType> repositoryPersonalityType,
             IRepository<UsersAssessmentResult> repositoryUsersAssessmentResult,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repository;
            _repositoryPersonalityType = repositoryPersonalityType;
            _repositoryUsersAssessmentResult = repositoryUsersAssessmentResult;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }





        public async Task<bool> Handle
            (UpdateUsersAssessmentResultPersonalityTypeCommand request,
            CancellationToken cancellationToken)
        {

            var ResultPersonalityType = await _repositoryPersonalityType.GetByIdAsync(request.dto.PersonalityTypeId);


            if (ResultPersonalityType == null)
            {
                throw new NotFoundException("PersonalityType not found");
            }
            var ResultUsersAssessmentResult = await _repositoryUsersAssessmentResult.GetByIdAsync(request.dto.UsersAssessmentResultId);
            if (ResultUsersAssessmentResult == null)
            {
                throw new NotFoundException("UsersAssessmentResult not found");
            }


            var result = await _repository.GetByIdAsync(request.dto.Id);

            if (result == null)
            {
                throw new NotFoundException("UsersAssessmentResultPersonalityType not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
