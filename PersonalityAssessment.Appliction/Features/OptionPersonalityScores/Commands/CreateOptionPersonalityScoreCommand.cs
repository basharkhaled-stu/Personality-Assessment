using MediatR;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands
{
    public record class CreateOptionPersonalityScoreCommand(CreateOptionPersonalityScoreDTO DTO)
        : IRequest<ReadOptionPersonalityScoreDTO>;

}
