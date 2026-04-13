using MediatR;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands
{
    public record DeleteOptionPersonalityScoreCommand(int id) : IRequest<bool>;
}
