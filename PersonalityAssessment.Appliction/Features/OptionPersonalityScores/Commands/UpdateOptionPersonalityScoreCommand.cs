using MediatR;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands
{
    public record UpdateOptionPersonalityScoreCommand(int id, UpdateOptionPersonalityScoreDTO dto)
       : IRequest<bool>;
}
