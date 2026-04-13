using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;
namespace PersonalityAssessment.Application.Features.Assessmentes.Commands.Handlers
{
    public class UpdateAssessmentCommandHandler :
        IRequestHandler<UpdateAssessmentCommand, bool>
    {
        private readonly IRepository<Assessment> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAssessmentCommandHandler
        (IRepository<Assessment> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle
            (UpdateAssessmentCommand request,
            CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                throw new NotFoundException("Assessment not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
