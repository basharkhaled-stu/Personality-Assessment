using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Handlers
{
    public class UpdateUsersAssessmentCommandHandler :
        IRequestHandler<UpdateUsersAssessmentCommand, bool>
    {
        private readonly IRepository<AssessmentStatus> _repositoryAssessmentStatus;
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;



        public UpdateUsersAssessmentCommandHandler(
          IRepository<UsersAssessment> repository,
            IRepository<AssessmentStatus> repositoryAssessmentStatus,


            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repository = repository;
            _repositoryAssessmentStatus = repositoryAssessmentStatus;

            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }




        public async Task<bool> Handle
            (UpdateUsersAssessmentCommand request,
            CancellationToken cancellationToken)
        {

            var ResultAssessmentStatus = await _repositoryAssessmentStatus.GetByIdAsync(request.dto.UserAssessmentStatusId);


            if (ResultAssessmentStatus == null)
            {
                throw new NotFoundException("AssessmentStatus not found");
            }

            var result = await _repository.GetByIdAsync(request.dto.Id);

            if (result == null)
            {
                throw new NotFoundException("UsersAssessment not found");
            }


            result.UserAssessmentStatusId = request.dto.UserAssessmentStatusId;



            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
