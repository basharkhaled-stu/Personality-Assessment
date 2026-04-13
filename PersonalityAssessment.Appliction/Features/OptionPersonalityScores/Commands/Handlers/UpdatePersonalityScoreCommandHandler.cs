using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands.Handlers
{
    public class UpdatePersonalityScoreCommandHandler :
        IRequestHandler<UpdateOptionPersonalityScoreCommand, bool>
    {
        private readonly IRepository<Option> _repositoryOption;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IRepository<OptionPersonalityScore> _repositoryOptionPersonalityScore;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;



        public UpdatePersonalityScoreCommandHandler(
           IRepository<Option> repositoryOption,
            IRepository<PersonalityType> repositoryPersonalityType,
            IRepository<OptionPersonalityScore> repositoryOptionPersonalityScore,

            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _repositoryOption = repositoryOption;
            _repositoryPersonalityType = repositoryPersonalityType;
            _repositoryOptionPersonalityScore = repositoryOptionPersonalityScore;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<bool> Handle
            (UpdateOptionPersonalityScoreCommand request,
            CancellationToken cancellationToken)
        {



            var ResultIOPtion = await _repositoryOption.GetByIdAsync(request.dto.OptionId);
            var ResultPersonalityType = await _repositoryPersonalityType.GetByIdAsync(request.dto.PersonalityTypeId);

            if (ResultIOPtion == null)
            {
                throw new NotFoundException("Option not found");
            }

            if (ResultPersonalityType == null)
            {
                throw new NotFoundException("PersonalityType not found");
            }

            var result = await _repositoryOptionPersonalityScore.GetByIdAsync(request.dto.Id);

            if (result == null)
            {
                throw new NotFoundException("OptionPersonalityScore not found");
            }

            _mapper.Map(request.dto, result);


            result.UpdatedAt = DateTime.UtcNow;
            _repositoryOptionPersonalityScore.Update(result);

            int key = await _unitOfWork.SaveChangesAsync();
            return key > 0;


        }
    }
}
