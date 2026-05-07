using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;
namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateappUserCommandHandler :
        IRequestHandler<UpdateOptionCommand, bool>
    {
        private readonly IRepository<Option> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateappUserCommandHandler
        (IRepository<Option> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle
            (UpdateOptionCommand request,
            CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.id);
            if (result is null)
            {
                throw new NotFoundException("Option not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repository.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
