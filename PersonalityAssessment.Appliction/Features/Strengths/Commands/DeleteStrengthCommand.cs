using MediatR;

namespace PersonalityAssessment.Application.Features.Strengths.Commands
{
    public record DeleteStrengthCommand(int id) : IRequest<bool>;
}
