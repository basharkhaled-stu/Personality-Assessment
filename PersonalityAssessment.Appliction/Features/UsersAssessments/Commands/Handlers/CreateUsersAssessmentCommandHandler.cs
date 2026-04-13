using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Handlers
{
    public class CreateUsersAssessmentCommandHandler
        : IRequestHandler<CreateUsersAssessmentCommand, ReadUsersAssessmentDTO>
    {
        private readonly IRepository<AssessmentStatus> _repositoryAssessmentStatus;
        private readonly IRepository<UsersAssessment> _repository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUsersAssessmentCommandHandler(
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



        public async Task<ReadUsersAssessmentDTO> Handle
            (CreateUsersAssessmentCommand request,
            CancellationToken cancellationToken)
        {

            var ResultAssessmentStatus = await _repositoryAssessmentStatus.GetByIdAsync(request.DTO.UserAssessmentStatusId);


            if (ResultAssessmentStatus == null)
            {
                throw new NotFoundException("AssessmentStatus not found");
            }




            var result = _mapper.Map<UsersAssessment>(request.DTO);

            result.UserId = request.UserId;


            await _repository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadUsersAssessmentDTO>(result);
        }
    }
}
