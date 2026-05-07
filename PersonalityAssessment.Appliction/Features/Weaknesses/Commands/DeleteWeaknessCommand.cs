using MediatR;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands
{
    public record DeleteWeaknessCommand(int id) : IRequest<bool>;
}
