using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands.Handlers
{
    public class UpdateUsersAssessmentResultCommandHandler :
        IRequestHandler<UpdateUsersAssessmentResultCommand, bool>
    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IRepository<UsersAssessment> _repositoryUsersAssessment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUsersAssessmentResultCommandHandler
        (IRepository<UsersAssessmentResult> repository,
            IRepository<UsersAssessment> repositoryUsersAssessment,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryUsersAssessment = repositoryUsersAssessment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle
            (UpdateUsersAssessmentResultCommand request,
            CancellationToken cancellationToken)
        {


            var resultUsersAssessment = await _repositoryUsersAssessment.GetByIdAsync(request.dto.UsersAssessmentId);
            if (resultUsersAssessment == null)
            {
                throw new NotFoundException("UsersAssessment Not Found");
            }
            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                throw new NotFoundException("UsersAssessmentResult not found");
            }

            _mapper.Map(request.dto, result);
            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int Key = await _unitOfWork.SaveChangesAsync();

            return Key > 0;
        }
    }
}
