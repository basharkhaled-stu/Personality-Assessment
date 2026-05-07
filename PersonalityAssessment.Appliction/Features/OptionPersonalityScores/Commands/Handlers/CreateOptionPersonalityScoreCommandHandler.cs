using AutoMapper;
using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;


namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands.Handlers
{
    public class CreateOptionPersonalityScoreCommandHandler
        : IRequestHandler<CreateOptionPersonalityScoreCommand, ReadOptionPersonalityScoreDTO>
    {
        private readonly IRepository<Option> _repositoryOption;
        private readonly IRepository<PersonalityType> _repositoryPersonalityType;
        private readonly IRepository<OptionPersonalityScore> _repositoryOptionPersonalityScore;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOptionPersonalityScoreCommandHandler(
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



        public async Task<ReadOptionPersonalityScoreDTO> Handle
            (CreateOptionPersonalityScoreCommand request,
            CancellationToken cancellationToken)
        {

            var ResultIOPtion = await _repositoryOption.GetByIdAsync(request.DTO.OptionId);
            var ResultPersonalityType = await _repositoryPersonalityType.GetByIdAsync(request.DTO.PersonalityTypeId);

            if (ResultIOPtion == null)
            {
                throw new NotFoundException("Option not found");
            }

            if (ResultPersonalityType == null)
            {
                throw new NotFoundException("PersonalityType not found");
            }

            var result = _mapper.Map<OptionPersonalityScore>(request.DTO);

            result.PersonalityType = ResultPersonalityType;
            result.Option = ResultIOPtion;



            await _repositoryOptionPersonalityScore.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadOptionPersonalityScoreDTO>(result);
        }
    }
}
