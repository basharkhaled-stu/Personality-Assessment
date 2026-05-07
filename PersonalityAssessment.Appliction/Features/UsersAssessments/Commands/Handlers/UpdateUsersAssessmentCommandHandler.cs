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
        // BUG FIX: was IRepository<AssessmentStatus> (wrong table)
        private readonly IRepository<UserAssessmentStatus> _repositoryUserAssessmentStatus;
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUsersAssessmentCommandHandler(
            IRepository<UsersAssessment> repository,
            IRepository<UserAssessmentStatus> repositoryUserAssessmentStatus,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUserAssessmentStatus = repositoryUserAssessmentStatus;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUsersAssessmentCommand request, CancellationToken cancellationToken)
        {
            var status = await _repositoryUserAssessmentStatus.GetByIdAsync(request.dto.UserAssessmentStatusId);
            if (status == null)
                throw new NotFoundException("UserAssessmentStatus not found");

            var result = await _repository.GetByIdAsync(request.dto.Id);
            if (result == null)
                throw new NotFoundException("UsersAssessment not found");

            result.UserAssessmentStatusId = request.dto.UserAssessmentStatusId;
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
